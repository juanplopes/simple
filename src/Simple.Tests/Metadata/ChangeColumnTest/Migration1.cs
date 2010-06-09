using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Fluent;
using Simple.Migrator.Framework;

namespace Simple.Tests.Metadata.ChangeColumnTest
{
    [Migration(1)]
    public class Migration1 : FluentMigration
    {
        public override void Up()
        {
            Schema.AddTable("t_change_column", t =>
            {
                t.AddString("string1").WithSize(123);
                t.AddInt32("int1");
            });

            Schema.ChangeTable("t_change_column", t =>
            {
                t.ChangeString("string1").WithSize(42);
            });
        }

        public override void Down()
        {
            Schema.RemoveTable("t_change_column");
        }
    }

}
