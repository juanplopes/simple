using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Schema.Migrator
{
    public class ColumnRemoveAction : ColumnAction
    {
        public ColumnRemoveAction(TableAction table, string name) : base(table, name) { }

        public override void Execute(ITransformationProvider provider)
        {
            provider.RemoveColumn(Table.Name, this.Name);
            base.Execute(provider);
        }
    }
}
