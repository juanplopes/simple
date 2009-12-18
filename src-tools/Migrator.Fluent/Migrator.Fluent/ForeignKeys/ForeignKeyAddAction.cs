using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent
{
    public class AddForeignKeyAccessor : ForeignKeyAction
    {
        public AddForeignKeyAccessor(TableAction table, string name, string foreignKeyColumn, string primaryKeyTable, string primaryKeyColumn)
            : base(table, name, foreignKeyColumn, primaryKeyTable, primaryKeyColumn) { }

        public override void Execute(ITransformationProvider provider)
        {
            provider.AddForeignKey(this.Name, Table.Name, this.FkColumn, this.PkTable, this.PkColumn);
        }
    }
}
