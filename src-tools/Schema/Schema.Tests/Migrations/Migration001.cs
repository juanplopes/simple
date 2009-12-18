using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schema.Migrator;
using Migrator.Framework;

namespace Schema.Tests.Migrations
{
    [Migration(1)]
    public class Migration001 : FluentMigration
    {
        public override void Up()
        {
            Schema.AddTable("t_simple_table", t =>
            {
                t.AddString("string1").WithSize(123);
                t.AddInt32("int1");
            });
        }

        public override void Down()
        {
            Schema.RemoveTable("t_simple_table");
        }
    }
}
