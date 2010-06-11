using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;

namespace Simple.Tests.Metadata.SimpleTableTest
{
    [Migration(1)]
    public class Migration1 : FluentMigration
    {
        public override void Up()
        {
            Schema.AddTable("t_all_types_1", t =>
            {
                t.AddString("string1").WithSize(123);
                t.AddInt32("int1");
            });
        }

        public override void Down()
        {
            Schema.RemoveTable("t_all_types_1");
        }
    }


}
