using System;
using System.Data;

namespace Simple.Metadata
{
    public class DbRelation : DbObject
    {
        public DbRelation(IDbSchemaProvider provider) : base(provider) { }

        public DbRelation(IDbSchemaProvider provider, DataRow row) : this(provider)
        {
            PkTableCatalog = GetValue<string>(row, "PK_TABLE_CATALOG");
            PkTableSchema = GetValue<string>(row, "PK_TABLE_SCHEMA");
            PkTableName = GetValue<string>(row, "PK_TABLE_NAME");
            PkColumnName = GetValue<string>(row, "PK_COLUMN_NAME");
            PkOrdinalPosition = GetValue<int>(row, "PK_ORDINAL_POSItION");

            FkTableCatalog = GetValue<string>(row, "FK_TABLE_CATALOG");
            FkTableSchema = GetValue<string>(row, "FK_TABLE_SCHEMA");
            FkTableName = GetValue<string>(row, "FK_TABLE_NAME");
            FkColumnName = GetValue<string>(row, "FK_COLUMN_NAME");
            FkOrdinalPosition = GetValue<int>(row, "FK_ORDINAL_POSItION");

            FkName = GetValue<string>(row, "FK_NAME");
        }

        public string PkTableCatalog { get; set; }
        public string PkTableSchema { get; set; }
        public string PkTableName { get; set; }
        public string PkColumnName { get; set; }
        public int PkOrdinalPosition { get; set; }

        public string FkTableCatalog { get; set; }
        public string FkTableSchema { get; set; }
        public string FkTableName { get; set; }
        public string FkColumnName { get; set; }
        public int FkOrdinalPosition { get; set; }


        public string FkName { get; set; }
    }
}
