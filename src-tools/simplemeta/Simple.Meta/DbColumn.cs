using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Simple.Meta
{
    public class DbColumn
    {
        public DbColumn() { }

        public DbColumn(DataRow ColumnRow)
        {
            this.AllowDBNull = (bool)ColumnRow["AllowDBNull"];
            if (ColumnRow.Table.Columns.Contains("BaseCatalogName"))
                if (ColumnRow["BaseCatalogName"] != DBNull.Value)
                    this.BaseCatalogName = ColumnRow["BaseCatalogName"].ToString();
            this.BaseColumnName = ColumnRow["BaseColumnName"].ToString();
            if (ColumnRow["BaseSchemaName"] != DBNull.Value)
                this.BaseSchemaName = ColumnRow["BaseSchemaName"].ToString();
            this.BaseTableName = ColumnRow["BaseTableName"].ToString();
            this.ColumnName = ColumnRow["ColumnName"].ToString();
            this.ColumnOrdinal = (int)ColumnRow["ColumnOrdinal"];
            this.ColumnSize = (int)ColumnRow["ColumnSize"];
            this.DataType = ColumnRow["DataType"].ToString();
            if (ColumnRow.Table.Columns.Contains("IsAutoIncrement"))
                this.IsAutoIncrement = (bool)ColumnRow["IsAutoIncrement"];
            this.IsKey = (bool)ColumnRow["IsKey"];
            this.IsLong = (bool)ColumnRow["IsLong"];
            if (ColumnRow.Table.Columns.Contains("IsReadOnly"))
                this.IsReadOnly = (bool)ColumnRow["IsReadOnly"];
            if (ColumnRow.Table.Columns.Contains("IsRowVersion"))
                this.IsRowVersion = (bool)ColumnRow["IsRowVersion"];
            this.IsUnique = (bool)ColumnRow["IsUnique"];
            this.NumericPrecision = Convert.ToInt32(ColumnRow["NumericPrecision"]);
            this.NumericScale = Convert.ToInt32(ColumnRow["NumericScale"]);
            if (ColumnRow.Table.Columns.Contains("ProviderType"))
                this.ProviderType = ColumnRow["ProviderType"].ToString();
        }

        public bool AllowDBNull { get; set; }
        public string BaseCatalogName { get; set; }
        public string BaseColumnName { get; set; }
        public string BaseSchemaName { get; set; }
        public string BaseTableName { get; set; }
        public string ColumnName { get; set; }
        public int ColumnOrdinal { get; set; }
        public int ColumnSize { get; set; }
        public string DataType { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsKey { get; set; }
        public bool IsLong { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsRowVersion { get; set; }
        public bool IsUnique { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericScale { get; set; }
        public string ProviderType { get; set; }

    }
}
