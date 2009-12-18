using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using System.Data;


namespace Migrator.Fluent
{
    public abstract class ColumnAction : InsideTableAction
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
        public int? Size { get; set; }
        public object DefaultValue { get; set; }
        public ColumnProperty Properties { get; set; }

        public IList<ForeignKeyAction> ForeignKeys { get; set; }

        public Framework.Column ToColumn()
        {
            var column = new Framework.Column(Name, Type);
            column.ColumnProperty = Properties;

            if (Size != null) column.Size = Size.Value;
            if (DefaultValue != null) column.DefaultValue = DefaultValue;
            return column;
        }

        public ColumnAction(TableAction table, string name) : base(table)
        {
            Name = name;
            ForeignKeys = new List<ForeignKeyAction>();
        }

        public ColumnAction(TableAction table, string name, DbType type)
            : this(table, name)
        {
            Type = type;
        }

        public ColumnAction WithSize(int size)
        {
            Size = size;
            return this;
        }

        public ColumnAction ToType(DbType type)
        {
            Type = type;
            return this;
        }

        public ColumnAction Default(object value)
        {
            DefaultValue = value;
            return this;
        }

        public ColumnAction WithProperties(params ColumnProperty[] properties)
        {
            foreach (var property in properties)
                Properties = Properties | property;

            return this;
        }

        public ColumnAction PrimaryKeyWithIdentity()
        {
            return WithProperties(ColumnProperty.PrimaryKeyWithIdentity);
        }

        public ColumnAction PrimaryKey()
        {
            return WithProperties(ColumnProperty.PrimaryKey);
        }

        public ColumnAction AutoForeignKey(string primaryKeyTable)
        {
            return AutoForeignKey(primaryKeyTable, null);
        }

        public ColumnAction AutoForeignKey(string primaryKeyTable, string tag)
        {
            return ForeignKey(
                Table.Database.Convention.ForeignKeyConstraint(Table.Name, string.Empty, primaryKeyTable, string.Empty, tag), 
                primaryKeyTable);
        }

        public ColumnAction ForeignKey(string name, string primaryKeyTable)
        {
            return ForeignKey(name, primaryKeyTable, Table.Database.Convention.PrimaryKeyColumn);
        }


        public ColumnAction ForeignKey(string name, string primaryKeyTable, string primaryKeyColumn)
        {
            this.ForeignKeys.Add(new AddForeignKeyAccessor(this.Table, name, this.Name, primaryKeyTable, primaryKeyColumn));
            return WithProperties(ColumnProperty.ForeignKey);
        }

        public ColumnAction Nullable()
        {
            return WithProperties(ColumnProperty.Null);
        }

        public ColumnAction NotNullable()
        {
            return WithProperties(ColumnProperty.NotNull);
        }

        public ColumnAction Indexed()
        {
            return WithProperties(ColumnProperty.Indexed);
        }

        public ColumnAction Unsigned()
        {
            return WithProperties(ColumnProperty.Unsigned);
        }

        public override void Execute(ITransformationProvider provider)
        {
            foreach (var foreignKey in this.ForeignKeys)
                foreignKey.Execute(provider);
        }
    }
}
