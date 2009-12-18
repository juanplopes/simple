using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent
{
    public class ForeignKeyRemoveAction : ForeignKeyAction
    {
        public ForeignKeyRemoveAction(TableAction table, string name) : base(table, name) { }

        public override void Execute(ITransformationProvider provider)
        {
            provider.RemoveForeignKey(Table.Name, this.Name);
        }
    }
}
