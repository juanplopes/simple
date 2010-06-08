using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public abstract class ForeignKeyAction : InsideTableAction
    {
        public string Name { get; set; }
        public IList<string> FkColumns { get; set; }
        public string PkTable { get; set; }
        public IList<string> PkColumns { get; set; }

        public ForeignKeyAction(TableAction table, string name)
            : base(table)
        {
            this.Name = name;
        }

        public ForeignKeyAction(TableAction table, string name, IList<string> foreignKeyColumns, string primaryKeyTable, IList<string> primaryKeyColumns)
            : this(table, name)
        {
            this.PkTable = primaryKeyTable;
            this.PkColumns = primaryKeyColumns;
            this.FkColumns = foreignKeyColumns;
        }

        public ForeignKeyAction(TableAction table, string name, string foreignKeyColumn, string primaryKeyTable, string primaryKeyColumn)
            : this(table, name, new[] { foreignKeyColumn }, primaryKeyTable, new[] { primaryKeyColumn })
        {
        }

    }
}
