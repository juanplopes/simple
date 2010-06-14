using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple.Metadata
{
    public class DbColumnName : ContextualizedObject
    {
        public string Name { get; set; }
        public DbTableName TableRef { get; set; }


        public DbColumnName(MetaContext context)
            : base(context)
        {
        }


        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbColumnName>()
                .Add(x => x.TableRef)
                .Add(x => x.Name);
        }
    }

    public class DbColumn : DbColumnName
    {
        public DbColumn(MetaContext context, DbTableName table, DataRow row)
            : base(context)
        {

            BaseColumnRef = new DbColumnName(context)
            {
                Name = row.GetValue<string>("BaseColumnName"),
                TableRef = new DbTableName(context)
                {
                    Catalog = row.GetValue<string>("BaseCatalogName"),
                    Name = row.GetValue<string>("BaseTableName"),
                    Schema = row.GetValue<string>("BaseSchemaName")
                }
            };

            Name = row.GetValue<string>("ColumnName");
            TableRef = table;

            AllowDBNull = row.GetValue<bool>("AllowDBNull");
            ColumnOrdinal = row.GetValue<int>("ColumnOrdinal");
            ColumnSize = row.GetValue<int>("ColumnSize");
            DataType = row.GetValue<Type>("DataType");
            IsAutoIncrement = row.GetValue<bool>("IsAutoIncrement");
            IsKey = row.GetValue<bool>("IsKey");
            IsLong = row.GetValue<bool>("IsLong");
            IsHidden = row.GetValue<bool>("IsHidden");
            IsReadOnly = row.GetValue<bool>("IsReadOnly");
            IsRowVersion = row.GetValue<bool>("IsRowVersion");
            IsUnique = row.GetValue<bool>("IsUnique");
            NumericPrecision = row.GetValue<int>("NumericPrecision");
            NumericScale = row.GetValue<int>("NumericScale");
            ProviderType = row.GetValue<string>("ProviderType");
            DataTypeName = row.GetValue<string>("DataTypeName");
        }

        public DbColumnName BaseColumnRef { get; set; }

        public bool AllowDBNull { get; set; }
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
        public DbType DbColumnType { get; set; }

        public string GetDisplayTypeName()
        {
            return GetDisplayTypeName(false);
        }

        public string GetDisplayTypeName(bool forceNullable)
        {
            return DataType.Name + ((AllowDBNull || forceNullable) && DataType.IsValueType ? "?" : "");
        }
    }
}
