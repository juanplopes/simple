using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Fluent;
using Simple.Migrator.Framework;

namespace Simple.Tests.Metadata.ChangeTableTest
{
    [Migration(2)]
    public class Migration2 : FluentMigration
    {
        public override void Up()
        {
            Schema.ChangeTable("t_simple_table", t =>
            {
                t.RemoveColumn("string1");
                t.RemoveColumn("int1");
                t.AddString("whatever").WithSize(1000);
            });
        }

        public override void Down()
        {
            Schema.ChangeTable("t_simple_table", t =>
            {
                t.AddString("string1").WithSize(123);
                t.AddInt32("int1");
                t.RemoveColumn("whatever");
            });
        }
    }

}
