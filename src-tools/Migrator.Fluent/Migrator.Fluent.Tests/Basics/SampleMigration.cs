using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent.Tests.Basics
{
    public class SampleMigration : FluentMigration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Schema.AddTable("tests", t =>
            {
                t.AddString("wisdom").WithSize(20);
                t.AddInt32("age").NotNullable();
            });

        }
    }
}
