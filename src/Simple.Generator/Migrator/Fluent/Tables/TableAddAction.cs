using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public class TableAddAction : TableAction
    {
        public TableAddAction(SchemaAction database, string name) : base(database, name) { }

        public override void Execute(ITransformationProvider provider)
        {
            var columnsAccessors = Actions.OfType<ColumnAddAction>();
            Actions = Actions.Except(columnsAccessors.OfType<IAction>()).ToList();

            provider.AddTable(this.Name, columnsAccessors.Select(x => x.ToColumn()).ToArray());
            base.Execute(provider);
        }
    }

}
