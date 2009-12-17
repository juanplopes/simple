using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Simple.Meta
{
    public class DbTable : DbObject
    {
        private DataTable _columnCache = null;

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

        private DataTable GetSchemaColumns(string tableSchema, string tableName)
        {
            if (_columnCache != null) return _columnCache;
            return _columnCache = Provider.GetTableColumns(tableSchema, tableName);
        }

        public IEnumerable<DbColumn> AllColumns
        {
            get
            {
                DataTable tableColumns = GetSchemaColumns(TableSchema, TableName);

                foreach (DataRow row in tableColumns.Rows)
                    yield return new DbColumn(Provider, row);
            }
        }

        public IEnumerable<DbColumn> VisibleColumns
        {
            get
            {
                return AllColumns.Where(x => !x.IsHidden);
            }
        }

        public IEnumerable<DbColumn> PrimaryKeys
        {
            get
            {
                return VisibleColumns.Where(x => x.IsKey);
            }
        }

        public IEnumerable<DbColumn> FieldColumns
        {
            get
            {
                return VisibleColumns.Except(PrimaryKeys);
            }
        }

        #endregion

    }
}
