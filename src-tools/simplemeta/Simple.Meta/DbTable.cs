using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Simple.Meta
{
    public class DbTable
    {
        public DbTable() { }

        public DbTable(DataRow TableRow)
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

    }
}
