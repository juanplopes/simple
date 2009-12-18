using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent
{
    public class UniqueConstraintAddAction : UniqueConstraintAction
    {
        public IList<ColumnAction> Columns { get; set; }

        public UniqueConstraintAddAction(TableAction table, string name, IList<ColumnAction> columns)
            : base(table, name)
        {
            Name = name;
            Columns = columns;
        }

        public override void Execute(ITransformationProvider provider)
        {
            provider.AddUniqueConstraint(Name, Table.Name,
                Columns.Select(x => x.Name).ToArray());
        }

    }
    
}
