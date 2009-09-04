using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using System.Data;

namespace Sample.Project.Migrations.Extensions
{
    public class CreateColumnAccessor : ColumnAccessor
    {
        public CreateColumnAccessor(TableAccessor table, string name, DbType type) : base(table, name, type) { }
        public override void Execute(Migration migration)
        {
            migration.Database.AddColumn(Table.TableName, this.ToColumn());
            base.Execute(migration);
        }
    }

    public class DropColumnAccessor : ColumnAccessor
    {
        public DropColumnAccessor(TableAccessor table, string name) : base(table, name) { }

        public override void Execute(Migration migration)
        {
            migration.Database.RemoveColumn(Table.TableName, this.Name);
            base.Execute(migration);
        }
    }

    public class ChangeColumnAccessor : ColumnAccessor
    {
        public ChangeColumnAccessor(TableAccessor table, string name, DbType type) : base(table, name, type) { }

        public override void Execute(Migration migration)
        {
            migration.Database.ChangeColumn(Table.TableName, this.ToColumn());
            base.Execute(migration);
        }
    }

    public abstract class ColumnAccessor : InsideTableAccessor
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
        public int? Size { get; set; }
        public object DefaultValue { get; set; }
        public ColumnProperty Properties { get; set; }

        public IList<ForeignKeyAccessor> ForeignKeys { get; set; }

        public Column ToColumn()
        {
            var column = new Column(Name, Type);
            column.ColumnProperty = Properties;

            if (Size != null) column.Size = Size.Value;
            if (DefaultValue != null) column.DefaultValue = DefaultValue;
            return column;
        }

        public ColumnAccessor(TableAccessor table, string name) : base(table)
        {
            Name = name;
            ForeignKeys = new List<ForeignKeyAccessor>();
        }

        public ColumnAccessor(TableAccessor table, string name, DbType type)
            : this(table, name)
        {
            Type = type;
        }

        public ColumnAccessor WithSize(int size)
        {
            Size = size;
            return this;
        }

        public ColumnAccessor ToType(DbType type)
        {
            Type = type;
            return this;
        }

        public ColumnAccessor Default(object value)
        {
            DefaultValue = value;
            return this;
        }

        public ColumnAccessor WithProperties(params ColumnProperty[] properties)
        {
            foreach (var property in properties)
                Properties = Properties | property;

            return this;
        }

        public ColumnAccessor PrimaryKeyWithIdentity()
        {
            return WithProperties(ColumnProperty.PrimaryKeyWithIdentity);
        }

        public ColumnAccessor AutoForeignKey(string primaryKeyTable)
        {
            return AutoForeignKey(primaryKeyTable, null);
        }

        public ColumnAccessor AutoForeignKey(string primaryKeyTable, string type)
        {
            return ForeignKey(ForeignKeyAccessor.DefaultForeignKeyName(Table.TableName, primaryKeyTable, type), primaryKeyTable);
        }

        public ColumnAccessor ForeignKey(string name, string primaryKeyTable)
        {
            return ForeignKey(name, primaryKeyTable, MigratorExtensions.DefaultPrimaryKey);
        }


        public ColumnAccessor ForeignKey(string name, string primaryKeyTable, string primaryKeyColumn)
        {
            this.ForeignKeys.Add(new CreateForeignKeyAccessor(this.Table, name, this.Name, primaryKeyTable, primaryKeyColumn));
            return WithProperties(ColumnProperty.ForeignKey);
        }

        public ColumnAccessor Nullable()
        {
            return WithProperties(ColumnProperty.Null);
        }

        public ColumnAccessor NotNullable()
        {
            return WithProperties(ColumnProperty.NotNull);
        }

        public ColumnAccessor Indexed()
        {
            return WithProperties(ColumnProperty.Indexed);
        }

        public override void Execute(Migration migration)
        {
            foreach (var foreignKey in this.ForeignKeys)
                foreignKey.Execute(migration);
        }
    }
}
