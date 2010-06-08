using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Migrator1 = Simple.Migrator.DbMigrator;
using Simple.Metadata;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;

namespace Simple.Tests.Metadata.Runs
{
    public class SimpleTableTest : BaseTest
    {
        public SimpleTableTest(DatabasesXml.Entry entry) : base(entry) { }


        [Migration(1)]
        public class Migration : FluentMigration
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

        public override IEnumerable<Type> GetMigrations()
        {
            yield return typeof(Migration);
        }

        public override IEnumerable<TableAddAction> GetTableDefinitions()
        {
            yield return TableDef("t_simple_table", t =>
            {
                t.AddInt32("id").PrimaryKey();
                t.AddString("string1").WithSize(123);
                t.AddInt32("int1");
            });
        }
    }
}
