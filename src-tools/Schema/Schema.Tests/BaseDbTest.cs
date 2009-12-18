using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Migrator1 = Migrator.Migrator;
using Schema.Tests.Migrations;
using Schema.Metadata;

namespace Schema.Tests
{
    public abstract class BaseDbTest
    {
        public abstract string ConnectionString { get; }
        public abstract string MigratorProvider { get; }
        public abstract string MetadataProvider { get; }
        
        Migrator1 _migrator = null;

        [TestFixtureSetUp]
        public void Setup()
        {
            _migrator = new Migrator1(MigratorProvider, ConnectionString, typeof(Migration001).Assembly);
            _migrator.MigrateToLastVersion();
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            _migrator = new Migrator1(MigratorProvider, ConnectionString, typeof(Migration001).Assembly);
            _migrator.MigrateTo(0);
        }

        protected void AssertColumn(IEnumerable<DbColumn> columns, string columnName, Type type)
        {
            Assert.AreEqual(1, columns.Count(x => x.ColumnName == columnName && x.DataType == type && x.DataTypeName != null));
        }

        [Test]
        public void SimpleTableTest()
        {
            var schema = new DbSchema(ConnectionString, MetadataProvider);
            var table = schema.GetTables().Single(x => x.TableName == "t_simple_table");

            Assert.AreEqual(3, table.Columns.Count());
            AssertColumn(table.Columns, "id", typeof(int));
            AssertColumn(table.Columns, "string1", typeof(string));
            AssertColumn(table.Columns, "int1", typeof(int));

            var primaryKeys = table.PrimaryKeyColumns;
            Assert.AreEqual(1, primaryKeys.Count());
            AssertColumn(primaryKeys, "id", typeof(int));

        }

    }
}
