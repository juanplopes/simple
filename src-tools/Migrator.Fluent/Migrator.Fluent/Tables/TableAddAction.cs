using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent
{
    public class TableAddAction : TableAction
    {
        public TableAddAction(SchemaBuilder database, string name) : base(database, name) { }

        public override void Execute(ITransformationProvider provider)
        {
            var columnsAccessors = Actions.OfType<ColumnAddAction>();

            var foreignKeys = columnsAccessors.SelectMany(x => x.ForeignKeys);
            var others = Actions.Where(x => !(x is ColumnAddAction));
            Actions = others.Union(foreignKeys.OfType<IAction>()).ToList();

            provider.AddTable(this.Name, columnsAccessors.Select(x => x.ToColumn()).ToArray());
            base.Execute(provider);
        }
    }

}
