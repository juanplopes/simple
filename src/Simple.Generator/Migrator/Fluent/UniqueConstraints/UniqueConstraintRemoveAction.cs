using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public class UniqueConstraintRemoveAction : UniqueConstraintAction
    {
        public UniqueConstraintRemoveAction(TableAction table, string name)
            : base(table, name)
        {

        }

        public override void Execute(ITransformationProvider provider)
        {
            provider.RemoveConstraint(this.Table.Name, this.Name);
        }
    }
}
