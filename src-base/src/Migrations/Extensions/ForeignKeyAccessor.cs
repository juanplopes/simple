using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Sample.Project.Migrations.Extensions
{
    public class DropForeignKeyAccessor : ForeignKeyAccessor
    {
        public DropForeignKeyAccessor(TableAccessor table, string name) : base(table, name) { }

        public override void Execute(Migration migration)
        {
            migration.Database.RemoveForeignKey(Table.TableName, this.Name);
        }
    }

    public class CreateForeignKeyAccessor : ForeignKeyAccessor
    {
        public CreateForeignKeyAccessor(TableAccessor table, string name, string foreignKeyColumn, string primaryKeyTable, string primaryKeyColumn) 
            : base(table, name, foreignKeyColumn, primaryKeyTable, primaryKeyColumn) { }

        public override void Execute(Migration migration)
        {
            migration.Database.AddForeignKey(this.Name, Table.TableName, this.FkColumn, this.PkTable, this.PkColumn);
        }
    }

    public abstract class ForeignKeyAccessor : InsideTableAccessor
    {
        public string Name { get; set; }
        public string FkColumn { get; set; }
        public string PkTable { get; set; }
        public string PkColumn { get; set; }

        public ForeignKeyAccessor(TableAccessor table, string name)
            : base(table)
        {
            this.Name = name;
        }

        public ForeignKeyAccessor(TableAccessor table, string name, string foreignKeyColumn, string primaryKeyTable, string primaryKeyColumn) 
            : this(table, name)
        {
            this.PkTable = primaryKeyTable;
            this.PkColumn = primaryKeyColumn;
            this.FkColumn = foreignKeyColumn;
        }

        public static string DefaultForeignKeyName(string fkTable, string pkTable, string type)
        {
            string res = fkTable + "_" + pkTable;
            if (type != null) res += "_" + type;
            return res + "_fk";
        }
    }
}
