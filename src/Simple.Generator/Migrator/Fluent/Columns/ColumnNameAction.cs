using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using System.Data;

namespace Simple.Migrator.Fluent
{
    public abstract class ColumnNameAction : InsideTableAction
    {
        public class Concrete : ColumnNameAction
        {
            public Concrete(TableAction table, string name) : base(table, name) { }

            public override void Execute(ITransformationProvider migration)
            {
            }
        }

        public string Name { get; set; }

        public ColumnNameAction(TableAction table, string name) : base(table)
        {
            Name = name;
        }

        public virtual ForeignKeyRelation LinkedTo(string column)
        {
            return new ForeignKeyRelation(this, column);
        }
       
    }
}
