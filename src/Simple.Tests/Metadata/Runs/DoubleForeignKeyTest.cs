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
    public class DoubleForeignKeyTest : BaseTest
    {
        public DoubleForeignKeyTest(DatabasesXml.Entry entry) : base(entry) { }

        [Migration(1)]
        public class Migration : FluentMigration
        {
            public override void Up()
            {
                Schema.AddTable("t_rel_1", false, t =>
                {
                    t.AddInt32("id1").PrimaryKey();
                    t.AddString("id2").PrimaryKey();

                    t.AddDateTime("field1");
                });

                Schema.AddTable("t_rel_2", false, t =>
                {
                    t.AutoForeignKey("t_rel_1",
                        t.AddInt32("id1").PrimaryKey().LinkedTo("id1"),
                        t.AddString("id2").PrimaryKey().LinkedTo("id2"));

                    t.UniqueColumns("uk_teste",
                        t.AddInt32("unique_field_1"),
                        t.AddSingle("unique_field_2"));

                    t.AddDateTime("field2");
                });

            }

            public override void Down()
            {
                Schema.RemoveTable("t_rel_2");
                Schema.RemoveTable("t_rel_1");
            }
        }

        public override IEnumerable<Type> GetMigrations()
        {
            yield return typeof(Migration);
        }



        public override IEnumerable<TableAddAction> GetTableDefinitions()
        {
            yield return TableDef("t_rel_1", t =>
            {
                t.AddInt32("id1").PrimaryKey();
                t.AddString("id2").PrimaryKey();

                t.AddDateTime("field1");
            });

            yield return TableDef("t_rel_2", t =>
            {
                t.ForeignKey("t_rel_2_t_rel_1_fk", "t_rel_1",
                    t.AddInt32("id1").PrimaryKey().LinkedTo("id1"),
                    t.AddString("id2").PrimaryKey().LinkedTo("id2"));

                t.UniqueColumns("uk_teste",
                    t.AddInt32("unique_field_1"),
                    t.AddSingle("unique_field_2"));

                t.AddDateTime("field2");
            });

        }
    }
}
