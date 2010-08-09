using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Fluent;
using Simple.Migrator.Framework;

namespace Simple.Tests.Metadata.AllTypesTest
{
    [Migration(1)]
    public class Migration1 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.AddTable("t_all_types_1", false, t =>
            {
                t.AddAnsiString("fieldAnsiString").PrimaryKey();
                t.AddBinary("fieldBinary").PrimaryKey();
                t.AddByte("fieldByte").PrimaryKey();
                t.AddBoolean("fieldBoolean").PrimaryKey();
                t.AddCurrency("fieldCurrency").PrimaryKey();
                t.AddDateTime("fieldDateTime").PrimaryKey();
                t.AddDecimal("fieldDecimal").PrimaryKey();
                t.AddDouble("fieldDouble").PrimaryKey();
                t.AddInt16("fieldInt16").PrimaryKey();
                t.AddInt32("fieldInt32").PrimaryKey();
                t.AddInt64("fieldInt64").PrimaryKey();
                t.AddSingle("fieldSingle").PrimaryKey();
                t.AddString("fieldString").PrimaryKey();
            });

            schema.AddTable("t_all_types_2", false, t =>
            {
                t.AutoForeignKey("t_all_types_1"
                                    , t.AddAnsiString("fieldAnsiString").PrimaryKey()
                                    , t.AddBinary("fieldBinary").PrimaryKey()
                                    , t.AddByte("fieldByte").PrimaryKey()
                                    , t.AddBoolean("fieldBoolean").PrimaryKey()
                                    , t.AddCurrency("fieldCurrency").PrimaryKey()
                                    , t.AddDateTime("fieldDateTime").PrimaryKey()
                                    , t.AddDecimal("fieldDecimal").PrimaryKey()
                                    , t.AddDouble("fieldDouble").PrimaryKey()
                                    , t.AddInt16("fieldInt16").PrimaryKey()
                                    , t.AddInt32("fieldInt32").PrimaryKey()
                                    , t.AddInt64("fieldInt64").PrimaryKey()
                                    , t.AddSingle("fieldSingle").PrimaryKey()
                                    , t.AddString("fieldString").PrimaryKey()
                                    );

            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.RemoveTable("t_all_types_2");
            schema.RemoveTable("t_all_types_1");
        }
    }

}
