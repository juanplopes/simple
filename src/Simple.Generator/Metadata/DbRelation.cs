using System;
using System.Data;
using Simple.Reflection;

namespace Simple.Metadata
{
    public class DbRelationName : ContextualizedObject
    {
        public DbColumnName PkColumnName { get; set; }
        public DbColumnName FkColumnName { get; set; }
        public string FkName { get; set; }

        public DbRelationName(MetaContext context)
            : base(context)
        { }

        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbRelationName>()
                .Add(x => x.PkColumnName)
                .Add(x => x.FkColumnName)
                .Add(x => x.FkName);
        }
    }


    public class DbRelation : DbRelationName
    {
        public int PkColumnPosition { get; set; }

        public DbRelation(MetaContext context) : base(context) { }

        public DbRelation(MetaContext context, DataRow row) : base(context)
        {
            PkColumnName = GetColumnInfo(context, row, "PK");
            PkOrdinalPosition = row.GetValue<int>("PK_ORDINAL_POSITION");

            FkColumnName = GetColumnInfo(context, row, "FK");
            FkOrdinalPosition = row.GetValue<int>("FK_ORDINAL_POSITION");

            FkName = row.GetValue<string>("FK_NAME");
        }

        private DbColumnName GetColumnInfo(MetaContext context, DataRow row, string type)
        {
            return new DbColumnName(context)
            {
                ColumnName = row.GetValue<string>(type + "_COLUMN_NAME"),
                TableName = new DbTableName(context)
                {
                    TableName = row.GetValue<string>(type + "_TABLE_NAME"),
                    TableSchema = row.GetValue<string>(type + "_TABLE_SCHEMA"),
                    TableCatalog = row.GetValue<string>(type + "_TABLE_CATALOG")
                }
            };

        }

        public int PkOrdinalPosition { get; set; }
        public int FkOrdinalPosition { get; set; }
    }
}
