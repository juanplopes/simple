using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Migrator;
using Simple.Metadata;
using Simple.Migrator.Fluent;
using Simple.Migrator.Providers;

namespace Simple.Tests.Metadata
{
    public abstract class BaseTest
    {
        public override string ToString()
        {
            return "{0} ({1})".AsFormat(this.Database.Provider, 
                this.GetType().Namespace.Replace(typeof(BaseTest).Namespace + ".", ""));
        }


        public DatabasesXml.Entry Database { get; protected set; }
        public BaseTest(DatabasesXml.Entry entry)
        {
            Database = entry;
        }

        public abstract IEnumerable<Type> GetMigrations();
        public abstract IEnumerable<TableAddAction> GetTableDefinitions();

        protected TableAddAction TableDef(string name, Action<TableAddAction> definition)
        {
            var table = new TableAddAction(null, name);
            definition(table);
            return table;
        }

        protected DbMigrator GetMigrator()
        {
            return new DbMigrator(
                new MigratorOptions(Database.Provider, Database.ConnectionString).AddTypes(GetMigrations()));
        }
        protected DbSchema GetSchema()
        {
            return new DbSchema(Database.Provider, Database.ConnectionString);
        }

        protected Dialect GetDialect()
        {
            return ProviderFactory.GetDialect(Database.Provider);
        }

        public virtual void Check()
        {
            var schema = GetSchema();
            var dialect = GetDialect();
            new TableAssertionHelper(schema, dialect).AssertTables(GetTableDefinitions());
        }


        public virtual void Setup()
        {
            using (var migrator = GetMigrator())
                migrator.MigrateToLastVersion("SchemaInfo");
        }

        public virtual void ExecuteAll()
        {
            try
            {
                Setup();
                Check();
            }
            finally
            {
                Teardown();
            }
        }

        public virtual void Teardown()
        {
            using (var migrator = GetMigrator())
                migrator.MigrateTo(0, "SchemaInfo");
        }

    }
}
