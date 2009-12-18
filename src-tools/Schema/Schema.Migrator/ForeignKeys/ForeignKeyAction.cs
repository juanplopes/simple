using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Schema.Migrator
{
    public abstract class ForeignKeyAction : InsideTableAction
    {
        public string Name { get; set; }
        public string FkColumn { get; set; }
        public string PkTable { get; set; }
        public string PkColumn { get; set; }

        public ForeignKeyAction(TableAction table, string name)
            : base(table)
        {
            this.Name = name;
        }

        public ForeignKeyAction(TableAction table, string name, string foreignKeyColumn, string primaryKeyTable, string primaryKeyColumn) 
            : this(table, name)
        {
            this.PkTable = primaryKeyTable;
            this.PkColumn = primaryKeyColumn;
            this.FkColumn = foreignKeyColumn;
        }
      
    }
}
