using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Migrator1 = Simple.Migrator.DbMigrator;
using Simple.Metadata;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;

namespace Simple.Tests.Metadata.DoubleForeignKeyTest
{
    public class Run : BaseTest
    {
        public Run(DatabasesXml.Entry entry) : base(entry) { }

        public override IEnumerable<Type> GetMigrations()
        {
            yield return typeof(Migration1);
        }

        public override IEnumerable<TableAddAction> GetTableDefinitions()
        {
            yield return TableDef("t_double_fk_1", t =>
            {
                t.AddInt32("id1").PrimaryKey();
                t.AddString("id2").PrimaryKey();

                t.AddDateTime("field1");
            });

            yield return TableDef("t_double_fk_2", t =>
            {
                t.ForeignKey("t_double_fk_2_t_double_fk_1_fk", "t_double_fk_1",
                    t.AddInt32("id1").PrimaryKey().LinkedTo("id1"),
                    t.AddString("id2").PrimaryKey().LinkedTo("id2")).ConstrainedAs(ForeignKeyConstraint.Cascade);

                t.UniqueColumns("uk_teste",
                    t.AddInt32("unique_field_1"),
                    t.AddSingle("unique_field_2"));

                t.AddDateTime("field2");
            });

        }
    }
}
