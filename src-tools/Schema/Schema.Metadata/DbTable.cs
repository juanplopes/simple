using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Schema.Metadata
{
    public class DbTable : DbObject
    {
        private IList<DbColumn> _columnCache = null;
        private IList<DbRelation> _fkCache = null;

        public DbTable(IDbSchemaProvider provider)
            : base(provider)
        {
        }

        public DbTable(IDbSchemaProvider provider, DataRow TableRow)
            : this(provider)
        {
            if (TableRow.Table.Columns.Contains("TABLE_CATALOG"))
                if (TableRow["TABLE_CATALOG"] != DBNull.Value)
                    this.TableCatalog = TableRow["TABLE_CATALOG"].ToString();
            if (TableRow.Table.Columns.Contains("TABLE_SCHEMA"))
                if (TableRow["TABLE_SCHEMA"] != DBNull.Value)
                    this.TableSchema = TableRow["TABLE_SCHEMA"].ToString();
            this.TableName = TableRow["TABLE_NAME"].ToString();
            this.TableType = TableRow["TABLE_TYPE"].ToString();
        }

        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }

        #region ' Columns and Primary Keys '

        private IEnumerable<DbColumn> GetSchemaColumns()
        {
            foreach (DataRow row in Provider.GetTableColumns(TableSchema, TableName).Rows)
                yield return new DbColumn(Provider, row);
        }

        private IEnumerable<DbRelation> GetRelations()
        {
            string where;
            if (!string.IsNullOrEmpty(TableSchema))
                where = string.Format("FK_TABLE_SCHEMA = '{0}' AND FK_TABLE_NAME = '{1}'", TableSchema, TableName);
            else
                where = string.Format("FK_TABLE_NAME = '{0}'", TableName);

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

        public IEnumerable<DbRelation> ForeignKeyColumns
        {
            //all outgoing relations
            get
            {
                if (_fkCache == null) _fkCache = GetRelations().ToList();
                return _fkCache;
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
            var excluded = new HashSet<string>(ForeignKeyColumns.Select(x => x.FkColumnName));
            return PrimaryKeyColumns.Where(x => !excluded.Contains(x.ColumnName));
        }

        public IEnumerable<DbColumn> GetFields()
        {
            //all columns except primary keys and foreign keys
            var excluded = new HashSet<string>(
                PrimaryKeyColumns.Select(x => x.ColumnName).Union(
                ForeignKeyColumns.Select(x => x.FkColumnName)));

            return Columns.Where(x => !excluded.Contains(x.ColumnName));
        }

        public IEnumerable<DbForeignKey> GetForeignKeys()
        {
            //all foreign keys columns grouped by fk name
            var fks = ForeignKeyColumns;
            return fks.GroupBy(x => x.FkName).Select(x => new DbForeignKey(Provider, x.Key, x.ToList()));
        }


        #endregion

    }
}
