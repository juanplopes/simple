using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;


namespace Schema.Migrator
{
    

    

    public abstract class UniqueConstraintAction : InsideTableAction
    {
        public string Name { get; set; }

        public UniqueConstraintAction(TableAction table, string name) : base(table)
        {
            Name = name;
        }

        
    }
}
