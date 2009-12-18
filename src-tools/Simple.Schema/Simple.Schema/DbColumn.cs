using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Simple.Meta
{
    public class DbColumn : DbObject
    {
        public DbColumn(IDbSchemaProvider provider) : base(provider) { }
        public DbColumn(IDbSchemaProvider provider, DataRow row)
            : this(provider)
        {
            this.AllowDBNull = (bool)row["AllowDBNull"];
            if (row.Table.Columns.Contains("BaseCatalogName"))
                if (row["BaseCatalogName"] != DBNull.Value)
                    this.BaseCatalogName = (string)row["BaseCatalogName"];
            this.BaseColumnName = row["BaseColumnName"].ToString();
            if (row["BaseSchemaName"] != DBNull.Value)
                this.BaseSchemaName = row["BaseSchemaName"].ToString();
            this.BaseTableName = row["BaseTableName"].ToString();
            this.ColumnName = row["ColumnName"].ToString();
            this.ColumnOrdinal = (int)row["ColumnOrdinal"];
            this.ColumnSize = (int)row["ColumnSize"];
            this.DataType = (Type)row["DataType"];
            if (row.Table.Columns.Contains("IsAutoIncrement"))
                this.IsAutoIncrement = (bool)row["IsAutoIncrement"];
            this.IsKey = (bool)row["IsKey"];
            this.IsLong = (bool)row["IsLong"];
            
            if (row.Table.Columns.Contains("IsHidden"))
                this.IsHidden = (bool)row["IsHidden"];
            
            if (row.Table.Columns.Contains("IsReadOnly"))
                this.IsReadOnly = (bool)row["IsReadOnly"];
            if (row.Table.Columns.Contains("IsRowVersion"))
                this.IsRowVersion = (bool)row["IsRowVersion"];
            this.IsUnique = (bool)row["IsUnique"];
            this.NumericPrecision = Convert.ToInt32(row["NumericPrecision"]);
            this.NumericScale = Convert.ToInt32(row["NumericScale"]);
            if (row.Table.Columns.Contains("ProviderType"))
                this.ProviderType = row["ProviderType"].ToString();
        }

        public bool AllowDBNull { get; set; }
        public string BaseCatalogName { get; set; }
        public string BaseColumnName { get; set; }
        public string BaseSchemaName { get; set; }
        public string BaseTableName { get; set; }
        public string ColumnName { get; set; }
        public int ColumnOrdinal { get; set; }
        public int ColumnSize { get; set; }
        public Type DataType { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsKey { get; set; }
        public bool IsLong { get; set; }
        public bool IsHidden { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsRowVersion { get; set; }
        public bool IsUnique { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericScale { get; set; }
        public string ProviderType { get; set; }

        public string DisplayTypeName
        {
            get
            {
                return DataType.Name + (AllowDBNull && DataType.IsValueType ? "?" : "");
            }
        }


    }
}
