using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Sample.Project.Migrations.Extensions
{
    public interface IAccessor
    {
        void Execute(Migration migration);
    }

    public static class MigratorExtensions
    {
        public const string DefaultPrimaryKey = "id";

        public static void CreateTable(this Migration migration, string name, Action<TableAccessor> definition)
        {
            CreateTable(migration, name, true, definition);
        }

        public static void CreateTable(this Migration migration, string name, bool addPrimaryKey, Action<TableAccessor> definition)
        {
            CreateTableAccessor table = new CreateTableAccessor(name);

            if (addPrimaryKey) 
                table.Integer(DefaultPrimaryKey).PrimaryKeyWithIdentity();
            
            definition(table);

            table.Execute(migration);
        }

        public static void DropTable(this Migration migration, string name)
        {
            new DropTableAccessor(name).Execute(migration);
        }

        public static void AlterTable(this Migration migration, string name, Action<TableAccessor> definition)
        {
            AlterTableAccessor table = new AlterTableAccessor(name);
            definition(table);
            table.Execute(migration);
        }
    }
}
