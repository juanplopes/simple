using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public class IndexRemoveAction : IndexAction
    {
        public IndexRemoveAction(TableAction table, string name)
            : base(table, name)
        {

        }

        public override void Execute(ITransformationProvider provider)
        {
            provider.RemoveIndex(this.Name, this.Table.Name);
        }
    }
}
