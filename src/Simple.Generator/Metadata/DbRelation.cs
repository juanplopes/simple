using System;
using System.Data;
using Simple.Reflection;

namespace Simple.Metadata
{
    public class DbRelationName : ContextualizedObject
    {
        public DbColumnName PkColumnRef { get; set; }
        public DbColumnName FkColumnRef { get; set; }
        public string Name { get; set; }

        public DbRelationName(MetaContext context)
            : base(context)
        { }

        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbRelationName>()
                .Add(x => x.PkColumnRef)
                .Add(x => x.FkColumnRef)
                .Add(x => x.Name);
        }
    }


    public class DbRelation : DbRelationName
    {
        public DbRelation(MetaContext context) : base(context) { }

        public DbRelation(MetaContext context, DataRow row) : base(context)
        {
            PkColumnRef = GetColumnInfo(context, row, "PK");
            PkOrdinalPosition = row.GetValue<int>("PK_ORDINAL_POSITION");

            FkColumnRef = GetColumnInfo(context, row, "FK");
            FkOrdinalPosition = row.GetValue<int>("FK_ORDINAL_POSITION");

            Name = row.GetValue<string>("FK_NAME");
        }

        private DbColumnName GetColumnInfo(MetaContext context, DataRow row, string type)
        {
            return new DbColumnName(context)
            {
                Name = row.GetValue<string>(type + "_COLUMN_NAME"),
                TableRef = new DbTableName(context)
                {
                    Name = row.GetValue<string>(type + "_TABLE_NAME"),
                    Schema = row.GetValue<string>(type + "_TABLE_SCHEMA"),
                    Catalog = row.GetValue<string>(type + "_TABLE_CATALOG")
                }
            };

        }

        public int PkOrdinalPosition { get; set; }
        public int FkOrdinalPosition { get; set; }
    }
}
