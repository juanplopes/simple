using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Fluent;
using Simple.Migrator.Framework;

namespace Simple.Tests.Metadata.SimpleForeignKeyTest
{
    [Migration(1)]
    public class Migration1 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.AddTable("t_simple_fk_1", t =>
               {
                   t.AddDateTime("field1");
               });

            schema.AddTable("t_simple_fk_2", t =>
            {
                t.AddInt32("auto_fk").Indexed().Default(1).AutoForeignKey("t_simple_fk_1");
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_simple_fk_2", t =>
            {
                t.RemoveColumn("auto_fk");
            });

            schema.RemoveTable("t_simple_fk_2");
            schema.RemoveTable("t_simple_fk_1");
        }
    }

}
