using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{   
    public class SchemaBuilder
    {
        public ITransformationProvider Provider { get; set; }
        public IConvention Convention { get; set; }

        public SchemaBuilder this[string provider]
        {
            get
            {
                return new SchemaBuilder(Provider[provider], Convention);
            }
        }

        public SchemaBuilder(ITransformationProvider provider) : this(provider, new DefaultConvention())
        {
        }

        public SchemaBuilder(ITransformationProvider provider, IConvention convention)
        {
            Provider = provider;
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

            table.Execute(Provider);

            return table;
        }

        public TableRenameAction RenameTable(string fromName, string toName)
        {
            var table = new TableRenameAction(fromName, toName);
            table.Execute(Provider);
            return table;
        }

        public TableRemoveAction RemoveTable(string name)
        {
            var table = new TableRemoveAction(this, name);
            table.Execute(Provider);
            return table;
        }

        public TableChangeAction ChangeTable(string name, Action<TableChangeAction> definition)
        {
            TableChangeAction table = new TableChangeAction(this, name);
            definition(table);
            table.Execute(Provider);
            return table;
        }
    }
}
