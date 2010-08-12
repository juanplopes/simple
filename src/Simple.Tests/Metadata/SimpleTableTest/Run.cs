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

namespace Simple.Tests.Metadata.SimpleTableTest
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
            yield return TableDef("t_all_types_1", t =>
            {
                t.AddInt32("id").PrimaryKey();
                t.AddString("string1").WithSize(123);
                t.AddInt32("int1");
            });
        }
    }
}
