using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using System.Data;

namespace Sample.Project.Migrations.Extensions
{
    public abstract class InsideTableAccessor : IAccessor
    {
        public TableAccessor Table { get; set; }
        public abstract void Execute(Migration migration);

        public InsideTableAccessor(TableAccessor table)
        {
            this.Table = table;
        }
    }

    public class DropTableAccessor : TableAccessor
    {
        public DropTableAccessor(string name) : base(name) { }

        public override void Execute(Migration migration)
        {
            migration.Database.RemoveTable(this.TableName);
            base.Execute(migration);
        }
    }

    public class CreateTableAccessor : TableAccessor
    {
        public CreateTableAccessor(string name) : base(name) { }

        public override void Execute(Migration migration)
        {
            var columnsAccessors = Operations.OfType<CreateColumnAccessor>();
            
            var foreignKeys = columnsAccessors.SelectMany(x => x.ForeignKeys);
            var others = Operations.Where(x=>!(x is CreateColumnAccessor));
            Operations = others.Union(foreignKeys.OfType<InsideTableAccessor>()).ToList();

            migration.Database.AddTable(this.TableName, columnsAccessors.Select(x=>x.ToColumn()).ToArray());
            base.Execute(migration);
        }
    }

    public class AlterTableAccessor : TableAccessor
    {
        public AlterTableAccessor(string name) : base(name) { }
    }

    public abstract class TableAccessor : IAccessor
    {
        public string TableName { get; set; }
        public IList<InsideTableAccessor> Operations { get; set; }

        public TableAccessor(string name)
        {
            TableName = name;
            Operations = new List<InsideTableAccessor>();
        }

        public ColumnAccessor AddColumn(string name, DbType type)
        {
            var column = new CreateColumnAccessor(this, name, type);
            Operations.Add(column);
            return column;
        }

        public ColumnAccessor ChangeColumn(string name, DbType type)
        {
            var column = new ChangeColumnAccessor(this, name, type);
            Operations.Add(column);
            return column;
        }

        public void DropColumn(string name)
        {
            Operations.Add(new DropColumnAccessor(this, name));
        }
        public void DropAutoForeignKey(string pkTable)
        {
            DropAutoForeignKey(pkTable, null);
        }

        public void DropAutoForeignKey(string pkTable, string type)
        {
            Operations.Add(new DropForeignKeyAccessor(this,
                ForeignKeyAccessor.DefaultForeignKeyName(this.TableName, pkTable, type)));
        }

        public void DropForeignKey(string name)
        {
            Operations.Add(new DropForeignKeyAccessor(this, name));
        }

        public ColumnAccessor String(string name)
        {
            return AddColumn(name, DbType.String);
        }

        public ColumnAccessor Integer(string name)
        {
            return AddColumn(name, DbType.Int32);
        }

        public ColumnAccessor Binary(string name)
        {
            return AddColumn(name, DbType.Binary);
        }

        public ColumnAccessor Boolean(string name)
        {
            return AddColumn(name, DbType.Boolean);
        }

        public void UniqueColumns(string name, params ColumnAccessor[] columns)
        {
            Operations.Add(new CreateUniqueConstraintAccessor(this, name, columns));
        }

        public void DropUniqueConstraint(string name)
        {
            Operations.Add(new DropUniqueConstraintAccessor(this, name));
        }

        public virtual void Execute(Migration migration)
        {
            foreach (var operation in this.Operations)
            {
                operation.Execute(migration);
            }
        }
    }
}
