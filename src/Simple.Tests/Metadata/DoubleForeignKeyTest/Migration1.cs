using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Fluent;
using Simple.Migrator.Framework;

namespace Simple.Tests.Metadata.DoubleForeignKeyTest
{
    [Migration(1)]
    public class Migration1 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.AddTable("t_double_fk_1", false, t =>
            {
                t.AddInt32("id1").PrimaryKey();
                t.AddString("id2").PrimaryKey();

                t.AddDateTime("field1");
            });

            schema.AddTable("t_double_fk_2", false, t =>
            {
                t.AutoForeignKey("t_double_fk_1",
                    t.AddInt32("id1").PrimaryKey().LinkedTo("id1"),
                    t.AddString("id2").PrimaryKey().LinkedTo("id2")).OnConflict(ForeignKeyConstraint.Cascade);

                t.UniqueColumns("uk_teste",
                    t.AddInt32("unique_field_1"),
                    t.AddSingle("unique_field_2"));

                t.AddDateTime("field2");
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.RemoveTable("t_double_fk_2");
            schema.RemoveTable("t_double_fk_1");
        }
    }

}
