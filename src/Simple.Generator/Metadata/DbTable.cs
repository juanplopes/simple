using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple.Metadata
{
    public class DbTableName : ContextualizedObject
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }

        public DbTableName(MetaContext context)
            : base(context)
        {
        }

        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbTableName>()
                .Add(x => x.TableCatalog)
                .Add(x => x.TableSchema)
                .Add(x => x.TableName);

        }
    }

    public class DbTable : DbTableName
    {
        public IList<DbColumn> AllColumns { get; internal set; }
        public IList<DbRelation> OutRelations { get; internal set; }
        public IList<DbRelation> InRelations { get; internal set; }

        public string QualifiedTableName { get; set; }
        public string TableType { get; set; }

        public DbTable(MetaContext context, DataRow row) : base(context)
        {
            this.TableCatalog = row.GetValue<string>("TABLE_CATALOG");
            this.TableSchema = row.GetValue<string>("TABLE_SCHEMA");
            this.TableName = row.GetValue<string>("TABLE_NAME");
            this.TableType = row.GetValue<string>("TABLE_TYPE");
        }



        #region ' Columns and Primary Keys '


        public IEnumerable<DbColumn> PrimaryKeyColumns
        {
            //all primary key columns
            get
            {
                return AllColumns.Where(x => x.IsKey);
            }
        }

        public IEnumerable<DbColumn> ForeignKeyColumns
        {
            get
            {
                var columns = new HashSet<DbColumnName>(OutRelations.Select(x => x.FkColumnName).Distinct());
                return AllColumns.Where(x => columns.Contains(x));
            }
        }

        public IEnumerable<DbColumn> PrimaryKeysExceptFk
        {
            get
            {
                return PrimaryKeyColumns.Except(ForeignKeyColumns);
            }
        }

        public IEnumerable<DbColumn> OrdinaryFields
        {
            get
            {
                return AllColumns.Except(PrimaryKeyColumns).Except(ForeignKeyColumns);
            }
        }

        public IEnumerable<DbManyToOne> ManyToOneRelations
        {
            get
            {
                //all foreign keys columns grouped by fk name
                HashSet<string> keys = new HashSet<string>(PrimaryKeyColumns.Select(x => x.ColumnName));

                var safe = OutRelations.GroupBy(x => x.FkTableName).ToDictionary(x => x.Key, x => x.Count());

                return OutRelations.GroupBy(x => x.FkName).Select(x => new DbManyToOne(x.Key, x.ToList())
                {
                    IsKey = x.Any(y => keys.Contains(y.FkColumnName)),
                    SafeNaming = safe[x.Select(y => y.FkTableName).First()] == 1
                });
            }
        }

        public IEnumerable<DbOneToMany> OneToManyRelations
        {
            get
            {
                //all foreign keys columns grouped by fk name

                var safe = InRelations.GroupBy(x => x.FkTableName).ToDictionary(x => x.Key, x => x.Count());

                return InRelations.GroupBy(x => x.FkName).Select(x => new DbOneToMany(x.Key, x.ToList())
                {
                    SafeNaming = safe[x.Select(y => y.FkTableName).First()] == 1
                });
            }
        }


        #endregion

    }
}
 