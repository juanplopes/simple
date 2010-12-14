using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Migrator1 = Simple.Migrator.DbMigrator;
using Simple.Metadata;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;

namespace Simple.Tests.Metadata.SimpleForeignKeyTest
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
            yield return TableDef("t_simple_fk_1", t =>
            {
                t.AddInt32("id").PrimaryKey();
                t.AddDateTime("field1");
            });

            yield return TableDef("t_simple_fk_2", t =>
            {
                t.AddInt32("id").PrimaryKey();

                t.ForeignKey("t_simple_fk_2_t_simple_fk_1_fk", "t_simple_fk_1",
                    t.AddInt32("auto_fk").Indexed().Default(1).LinkedTo("id"))
                    .OnConflict(ForeignKeyConstraint.Cascade);

            });

        }
    }
}
