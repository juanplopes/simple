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
        public string Catalog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }

        public DbTableName(MetaContext context)
            : base(context)
        {
        }

        public DbTableName(MetaContext context, string name)
            : base(context)
        {
            var parts = name.Split('.').Reverse().ToList();
            if (parts.Count > 0) Name = parts[0];
            if (parts.Count > 1) Schema = parts[1];
            if (parts.Count > 2) Catalog = parts[2];
        }

        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbTableName>()
                .Add(x => x.Catalog)
                .Add(x => x.Schema)
                .Add(x => x.Name);

        }
    }

    public class DbTable : DbTableName
    {
        public IList<DbColumn> AllColumns { get; internal set; }
        public IList<DbRelation> OutRelations { get; internal set; }
        public IList<DbRelation> InRelations { get; internal set; }

        public string QualifiedTableName { get; set; }
        public string TableType { get; set; }

        public DbTable(MetaContext context, DataRow row)
            : base(context)
        {
            this.Catalog = row.GetValue<string>("TABLE_CATALOG");
            this.Schema = row.GetValue<string>("TABLE_SCHEMA");
            this.Name = row.GetValue<string>("TABLE_NAME");
            this.TableType = row.GetValue<string>("TABLE_TYPE");
        }

        public void ExecuteCache()
        {
            AllColumns = Context.ColumnsByTable[this].ToList();
            OutRelations = Context.OutRelationsByTable[this].ToList();
            InRelations = Context.InRelationsByTable[this].ToList();
            ExecuteOutFkCache();
            ExecuteInFkCache();
        }

        public void ExecuteOutFkCache()
        {
            //all foreign keys columns grouped by fk name
            var keys = new HashSet<DbColumnName>(PrimaryKeyColumns.Select(x => x as DbColumnName));

            var safe = OutRelations.GroupBy(x => x.FkColumnRef.TableRef).ToDictionary(x => x.Key, x => x.Count());

            ManyToOneRelations = OutRelations.GroupBy(x => x.Name).Select(x => new DbManyToOne(Context, x.ToList())
            {
                IsKey = x.Any(y => keys.Contains(y.FkColumnRef)),
                SafeNaming = safe[x.First().FkColumnRef.TableRef] == 1
            });
        }

        public void ExecuteInFkCache()
        {
            //all foreign keys columns grouped by fk name

            var safe = InRelations.GroupBy(x => x.FkColumnRef.TableRef).ToDictionary(x => x.Key, x => x.Count());

            OneToManyRelations = InRelations.GroupBy(x => x.Name).Select(x => new DbOneToMany(Context, x.ToList())
            {
                SafeNaming = safe[x.First().FkColumnRef.TableRef] == 1
            });
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
                var columns = new HashSet<DbColumnName>(OutRelations.Select(x => x.FkColumnRef).Distinct());
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

        public IEnumerable<DbManyToOne> ManyToOneRelations { get; protected set; }

        public IEnumerable<DbOneToMany> OneToManyRelations { get; protected set;}

        #endregion

    }
}
