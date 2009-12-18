using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using System.Data;

namespace Migrator.Fluent
{


    public abstract partial class TableAction : IAction
    {
        public SchemaBuilder Database { get; set; }
        public string Name { get; set; }
        public IList<IAction> Actions { get; set; }

        public TableAction(SchemaBuilder database, string name)
        {
            Name = name;
            Database = database;
            Actions = new List<IAction>();
        }

        public ColumnAction AddColumn(string name, DbType type)
        {
            var column = new ColumnAddAction(this, name, type);
            Actions.Add(column);
            return column;
        }

        


        public void UniqueColumns(string name, params ColumnAction[] columns)
        {
            Actions.Add(new UniqueConstraintAddAction(this, name, columns));
        }

       
        public virtual void Execute(ITransformationProvider provider)
        {
            foreach (var action in this.Actions)
            {
                action.Execute(provider);
            }
        }
    }
}
