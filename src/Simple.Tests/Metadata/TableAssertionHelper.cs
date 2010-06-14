using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;
using NUnit.Framework;
using System.Linq.Expressions;
using Simple.Migrator.Fluent;
using Simple.Migrator.Framework;
using Simple.Migrator;
using Simple.Migrator.Providers;

namespace Simple.Tests.Metadata
{
    public class TableAssertionHelper
    {
        public DbSchema Schema { get; protected set; }
        public Dialect Dialect { get; protected set; }

        public TableAssertionHelper(DbSchema schema, Dialect dialect)
        {
            this.Schema = schema;
            this.Dialect = dialect;
        }

        public void AssertTables(IEnumerable<TableAddAction> tables)
        {
            var defTables = tables.ToList();
            
            var actualTables = Schema.GetTables(defTables.Select(x => x.Name).ToArray())
                .ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);

            foreach (var table in tables)
            {
                AssertSingleTable(table,
                    tables.SelectMany(x => x.Actions.OfType<ForeignKeyAddAction>().Where(y => y.PkTable == table.Name)).ToList(),
                    actualTables[table.Name]);
            }
        }

        protected void AssertSingleTable(TableAddAction table, IList<ForeignKeyAddAction> oneToManyList, DbTable actualTable)
        {
            var expectedColumns = table.Actions.OfType<ColumnAddAction>().ToList();
            var expectedPrimaryKeys = expectedColumns.Where(x => (x.Properties & ColumnProperty.PrimaryKey) != 0).ToList();
            var expectedForeignKeys = table.Actions.OfType<ForeignKeyAddAction>().ToList();

            var fkColumnCount = expectedForeignKeys.Sum(x => x.FkColumns.Count);
            var pkColumnCount = expectedPrimaryKeys.Count;
            var fkPkColumnCount = expectedColumns.Count(x => (x.Properties & ColumnProperty.ForeignKey) != 0);
            var columnCount = expectedColumns.Count;

            Assert.AreEqual(columnCount - fkColumnCount - pkColumnCount + fkPkColumnCount, actualTable.OrdinaryFields.Count());
            Assert.AreEqual(fkColumnCount, actualTable.ForeignKeyColumns.Count());
            Assert.AreEqual(pkColumnCount - fkPkColumnCount, actualTable.PrimaryKeysExceptFk.Count());

            var actualColumns = actualTable.AllColumns.OrderBy(x => x.ColumnOrdinal).ToList();
            var actualPrimaryKeys = actualTable.PrimaryKeyColumns.OrderBy(x => x.ColumnOrdinal).ToList();
            var actualForeignKeys = actualTable.ManyToOneRelations.ToList();
            var actualOneToMany = actualTable.OneToManyRelations.ToList();

            AssertItems(expectedColumns, actualColumns, AssertSingleColumn, "columns");
            AssertItems(expectedPrimaryKeys, actualPrimaryKeys, AssertSingleColumn, "primary keys");
            AssertItems(expectedForeignKeys, actualForeignKeys, AssertSingleRelation, "foreign keys");
            AssertItems(oneToManyList, actualOneToMany, AssertSingleRelation, "one to many relations");
        }

        protected void AssertItems<T1, T2>(IList<T1> list1, IList<T2> list2, Action<T1, T2> asserter, string name)
        {
            Assert.AreEqual(list1.Count, list2.Count, name);
            for (int i = 0; i < list1.Count; i++)
                asserter(list1[i], list2[i]);
        }

        protected void AssertSingleColumn(ColumnAddAction column, DbColumn actualColumn)
        {
            string columnId = string.Format("{0}.{1}", column.Table.Name, column.Name);
            StringAssert.AreEqualIgnoringCase(column.Name, actualColumn.Name, "column name");

            string type = Dialect.GetTypeName(column.Type);
            StringAssert.StartsWith(actualColumn.DataTypeName.ToUpper(), type.ToUpper());
            //Assert.AreEqual(column.Type, actualColumn.GetDbColumnType(), "column type for {0}", columnId);
            if (column.Size != null)
                Assert.AreEqual(column.Size, actualColumn.ColumnSize, "column size for {0}", columnId);
        }

        protected void AssertSingleRelation(ForeignKeyAddAction fk, DbForeignKey actualForeignKey)
        {
            StringAssert.AreEqualIgnoringCase(fk.Name, actualForeignKey.Name);
            Assert.AreEqual(fk.FkColumns.Count, fk.PkColumns.Count);
            var fkColumns = actualForeignKey.Columns.OrderBy(x => x.FkOrdinalPosition).ToList();
            for (int i = 0; i < fk.FkColumns.Count; i++)
            {
                StringAssert.AreEqualIgnoringCase(fk.FkColumns[i], fkColumns[i].FkColumnRef.Name);
                StringAssert.AreEqualIgnoringCase(fk.Table.Name, actualForeignKey.Columns[i].FkColumnRef.TableRef.Name);
                StringAssert.AreEqualIgnoringCase(fk.PkColumns[i], fkColumns[i].PkColumnRef.Name);
                StringAssert.AreEqualIgnoringCase(fk.PkTable, actualForeignKey.Columns[i].PkColumnRef.TableRef.Name);
            }
        }
    }
}
