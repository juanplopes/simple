using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Simple.Metadata
{
    public class DbTable : DbObject
    {
        private IList<DbColumn> _columnCache = null;
        private IList<DbRelation> _outFkCache = null;
        private IList<DbRelation> _inFkCache = null;

        public DbTable(IDbSchemaProvider provider)
            : base(provider)
        {
        }

        public DbTable(IDbSchemaProvider provider, DataRow row)
            : this(provider)
        {
            TableCatalog = GetValue<string>(row, "TABLE_CATALOG");
            TableSchema = GetValue<string>(row, "TABLE_SCHEMA");
            TableName = GetValue<string>(row, "TABLE_NAME");
            TableType = GetValue<string>(row, "TABLE_TYPE");
        }

        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }

        public string QualifiedTableName
        {
            get
            {
                return Provider.QualifiedTableName(this.TableSchema, this.TableName);
            }
        }

        #region ' Columns and Primary Keys '

        private IEnumerable<DbColumn> GetSchemaColumns()
        {
            foreach (DataRow row in Provider.GetTableColumns(TableSchema, TableName).Rows)
                yield return new DbColumn(Provider, row);
        }

        private IEnumerable<DbRelation> GetRelations(string type)
        {
            string where = string.Format(type + "_TABLE_NAME = '{0}' AND PK_ORDINAL_POSITION = FK_ORDINAL_POSITION", TableName);

            if (!string.IsNullOrEmpty(TableSchema))
                where += string.Format(" AND " + type + "_TABLE_SCHEMA = '{0}'", TableSchema);

            foreach (var row in Provider.GetConstraints().Select(where))
                yield return new DbRelation(Provider, row);
        }

        protected IEnumerable<DbColumn> AllColumns
        {
            //all columns including hidden
            get
            {
                if (_columnCache == null) _columnCache = GetSchemaColumns().ToList();
                return _columnCache;
            }
        }


        public IEnumerable<DbColumn> Columns
        {
            //all columns, except hidden
            get
            {
                return AllColumns.Where(x => !x.IsHidden);
            }
        }

        public IEnumerable<DbRelation> ManyToOneColumns
        {
            //all outgoing relations
            get
            {
                if (_outFkCache == null) _outFkCache = GetRelations("FK").ToList();
                return _outFkCache;
            }
        }

        public IEnumerable<DbRelation> OneToManyColumns
        {
            //all outgoing relations
            get
            {
                if (_inFkCache == null) _inFkCache = GetRelations("PK").ToList();
                return _inFkCache;
            }
        }

        public IEnumerable<DbColumn> PrimaryKeyColumns
        {
            //all primary key columns
            get
            {
                return Columns.Where(x => x.IsKey);
            }
        }

        public IEnumerable<DbColumn> GetKeyFields()
        {
            //all primary keys not included in a foreign key
            var excluded = new HashSet<string>(ManyToOneColumns.Select(x => x.FkColumnName));
            return PrimaryKeyColumns.Where(x => !excluded.Contains(x.ColumnName));
        }

        public IEnumerable<DbColumn> GetFields()
        {
            //all columns except primary keys and foreign keys
            var excluded = new HashSet<string>(
                PrimaryKeyColumns.Select(x => x.ColumnName).Union(
                ManyToOneColumns.Select(x => x.FkColumnName)));

            return Columns.Where(x => !excluded.Contains(x.ColumnName));
        }

        public IEnumerable<DbManyToOne> GetManyToOneFields()
        {
            //all foreign keys columns grouped by fk name
            HashSet<string> keys = new HashSet<string>(PrimaryKeyColumns.Select(x => x.ColumnName));
            var safe = ManyToOneColumns.GroupBy(x => x.FkTableName).ToDictionary(x => x.Key, x => x.Count());

            return ManyToOneColumns.GroupBy(x => x.FkName).Select(x => new DbManyToOne(Provider, x.Key, x.ToList())
            {
                IsKey = x.Any(y => keys.Contains(y.FkColumnName)),
                SafeNaming = safe[x.Select(y => y.FkTableName).First()] == 1
            });
        }

        public IEnumerable<DbOneToMany> GetOneToManyFields()
        {
            //all foreign keys columns grouped by fk name

            var safe = OneToManyColumns.GroupBy(x => x.FkTableName).ToDictionary(x => x.Key, x => x.Count());

            return OneToManyColumns.GroupBy(x => x.FkName).Select(x => new DbOneToMany(Provider, x.Key, x.ToList())
            {
                SafeNaming = safe[x.Select(y => y.FkTableName).First()] == 1
            });
        }


        #endregion

    }
}
