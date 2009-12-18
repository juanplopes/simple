using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Schema.Migrator
{
    public class TableChangeAction : TableAction
    {
        public TableChangeAction(SchemaBuilder database, string name) : base(database, name) { }

        public ColumnAction ChangeColumn(string name, DbType type)
        {
            var column = new ColumnChangeAction(this, name, type);
            Actions.Add(column);
            return column;
        }

        public void RemoveColumn(string name)
        {
            Actions.Add(new ColumnRemoveAction(this, name));
        }
        public void RemoveAutoForeignKey(string pkTable)
        {
            RemoveAutoForeignKey(pkTable, string.Empty);
        }

        public void RemoveAutoForeignKey(string pkTable, string tag)
        {
            RemoveForeignKey(Database.Convention.ForeignKeyConstraint(Name, string.Empty, pkTable, string.Empty, tag));
        }

        public void RemoveForeignKey(string name)
        {
            Actions.Add(new ForeignKeyRemoveAction(this, name));
        }

        public void RemoveUniqueConstraint(string name)
        {
            Actions.Add(new UniqueConstraintRemoveAction(this, name));
        }

    }


}
