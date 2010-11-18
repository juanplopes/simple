using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Migrator.Fluent
{
   public abstract class IndexAction : InsideTableAction
    {
        public string Name { get; set; }

        public IndexAction(TableAction table, string name)
            : base(table)
        {
            Name = name;
        }
    }
}
