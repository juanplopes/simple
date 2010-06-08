using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Simple.Migrator.Fluent
{
    public partial class TableChangeAction : TableAction
    {
        public TableChangeAction(SchemaBuilder database, string name) : base(database, name) { }

        public ColumnChangeAction ChangeColumn(string name, DbType type)
        {
            var column = new ColumnChangeAction(this, name, type);
            Actions.Add(column);
            return column;
        }

        public ColumnRenameAction RenameColumn(string fromName, string toName)
        {
            var column = new ColumnRenameAction(this, fromName, toName);
            Actions.Add(column);
            return column;
        }

        public ColumnRemoveAction RemoveColumn(string name)
        {
            var column = new ColumnRemoveAction(this, name);
            Actions.Add(column);
            return column;
        }
        public ForeignKeyRemoveAction RemoveAutoForeignKey(string pkTable)
        {
            return RemoveAutoForeignKey(pkTable, string.Empty);
        }

        public ForeignKeyRemoveAction RemoveAutoForeignKey(string pkTable, string tag)
        {
            return RemoveForeignKey(Database.Convention.ForeignKeyConstraint(Name, string.Empty, pkTable, string.Empty, tag));
        }

        public ForeignKeyRemoveAction RemoveForeignKey(string name)
        {
            var action = new ForeignKeyRemoveAction(this, name);
            Actions.Add(action);
            return action;
        }

        public UniqueConstraintRemoveAction RemoveUniqueConstraint(string name)
        {
            var action = new UniqueConstraintRemoveAction(this, name);
            Actions.Add(action);
            return action;
        }

    }


}
