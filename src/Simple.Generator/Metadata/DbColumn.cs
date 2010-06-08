using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Simple.Metadata
{
    public class DbColumn : DbObject
    {
        public DbColumn(IDbSchemaProvider provider) : base(provider) { }
        public DbColumn(IDbSchemaProvider provider, DataRow row)
            : this(provider)
        {
            AllowDBNull = GetValue<bool>(row, "AllowDBNull");
            BaseCatalogName = GetValue<string>(row, "BaseCatalogName");
            BaseColumnName = GetValue<string>(row, "BaseColumnName");
            BaseSchemaName = GetValue<string>(row, "BaseSchemaName");
            BaseTableName = GetValue<string>(row, "BaseTableName");
            ColumnName = GetValue<string>(row, "ColumnName");
            ColumnOrdinal = GetValue<int>(row, "ColumnOrdinal");
            ColumnSize = GetValue<int>(row, "ColumnSize");
            DataType = GetValue<Type>(row, "DataType");
            IsAutoIncrement = GetValue<bool>(row, "IsAutoIncrement");
            IsKey = GetValue<bool>(row, "IsKey");
            IsLong = GetValue<bool>(row, "IsLong");
            IsHidden = GetValue<bool>(row, "IsHidden");
            IsReadOnly = GetValue<bool>(row, "IsReadOnly");
            IsRowVersion = GetValue<bool>(row, "IsRowVersion");
            IsUnique = GetValue<bool>(row, "IsUnique");
            NumericPrecision = GetValue<int>(row, "NumericPrecision");
            NumericScale = GetValue<int>(row, "NumericScale");
            ProviderType = GetValue<string>(row, "ProviderType");
            DataTypeName = GetValue<string>(row, "DataTypeName");
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
        public string DataTypeName { get; set; }
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

        public string GetDisplayTypeName()
        {
            return GetDisplayTypeName(false);
        }

        public DbType GetDbColumnType()
        {
            return Provider.GetDbColumnType(ProviderType);
        }

        public string GetDisplayTypeName(bool forceNullable)
        {
            return DataType.Name + ((AllowDBNull || forceNullable) && DataType.IsValueType ? "?" : "");
        }
    }
}
