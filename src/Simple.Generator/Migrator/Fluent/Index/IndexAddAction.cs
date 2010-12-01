using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public class IndexAddAction : IndexAction
    {
        public IList<string> Columns { get; set; }

        public IndexAddAction(TableAction table, string name, IList<string> columns)
            : base(table, name)
        {
            Name = name;
            Columns = columns;
        }

        public override void Execute(ITransformationProvider provider)
        {
            provider.AddIndex(Name, Table.Name,
                Columns.Select(x => x).ToArray());
        }
    }
}
