using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Schema.Migrator
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

        public void AddTable(string name, Action<TableAction> definition)
        {
            AddTable(name, true, definition);
        }

        public void AddTable(string name, bool addPrimaryKey, Action<TableAction> definition)
        {
            TableAddAction table = new TableAddAction(this, name);

            if (addPrimaryKey)
                table.AddInt32(Convention.PrimaryKeyColumn).PrimaryKeyWithIdentity();

            definition(table);

            table.Execute(Provider);
        }

        public void RemoveTable(string name)
        {
            new TableRemoveAction(this, name).Execute(Provider);
        }

        public void ChangeTable(string name, Action<TableChangeAction> definition)
        {
            TableChangeAction table = new TableChangeAction(this, name);
            definition(table);
            table.Execute(Provider);
        }
    }
}
