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
                IEnumerable<DataRow> columns = null;

                if (tableColumns.Columns.Contains("IsHidden"))
                    columns = tableColumns.Select("IsHidden = 0 OR IsHidden IS NULL");
                else
                    columns = tableColumns.Rows.OfType<DataRow>();

                foreach (var row in columns)
                    yield return new DbColumn(Provider, row);
            }
        }

        public IEnumerable<DbColumn> PrimaryKeys
        {
            get
            {
                foreach (var row in GetSchemaColumns(TableSchema, TableName).Select("IsKey = true"))
                    yield return new DbColumn(Provider, row);
            }
        }

        public IEnumerable<DbColumn> FieldColumns
        {
            get
            {
                var ignoreColumns = new HashSet<string>();
                foreach (var column in PrimaryKeys)
                    ignoreColumns.Add("'" + column.ColumnName + "'");

                string where = "1=1";
                if (ignoreColumns.Count > 0)
                    where = "ColumnName NOT IN ( " + string.Join(", ", ignoreColumns.ToArray()) + " )";

                foreach (var row in GetSchemaColumns(TableSchema, TableName).Select(where))
                    yield return new DbColumn(Provider, row);
            }
        }

        #endregion

    }
}
