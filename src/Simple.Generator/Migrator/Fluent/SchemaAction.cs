using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public class SchemaAction : IAction
    {
        public IConvention Convention { get; set; }
        public IList<IAction> Actions { get; set; }

        public SchemaAction(IConvention convention)
        {
            Actions = new List<IAction>();
            Convention = convention;
        }

        public TableAddAction AddTable(string name, Action<TableAction> definition)
        {
            return AddTable(name, true, definition);
        }

        public TableAddAction AddTable(string name, bool addPrimaryKey, Action<TableAction> definition)
        {
            TableAddAction table = new TableAddAction(this, name);

            if (addPrimaryKey)
                table.AddInt32(Convention.PrimaryKeyColumn(name)).PrimaryKeyWithIdentity();

            definition(table);
            Actions.Add(table);

            return table;
        }

        public TableRenameAction RenameTable(string fromName, string toName)
        {
            var table = new TableRenameAction(fromName, toName);

            Actions.Add(table);

            return table;
        }

        public TableRemoveAction RemoveTable(string name)
        {
            var table = new TableRemoveAction(this, name);
            Actions.Add(table);

            return table;
        }

        public TableChangeAction ChangeTable(string name, Action<TableChangeAction> definition)
        {
            TableChangeAction table = new TableChangeAction(this, name);
            definition(table);
            Actions.Add(table);
            return table;
        }

        public FreeAction Do(Action<ITransformationProvider> action)
        {
            var item = new FreeAction(action);
            Actions.Add(item);
            return item;
        }

        #region IAction Members

        public void Execute(ITransformationProvider provider)
        {
            foreach (var action in Actions)
                action.Execute(provider);
        }

        #endregion
    }
}
