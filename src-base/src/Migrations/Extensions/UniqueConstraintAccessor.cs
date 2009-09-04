using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Sample.Project.Migrations.Extensions
{
    public class DropUniqueConstraintAccessor : UniqueConstraintAccessor
    {
        public DropUniqueConstraintAccessor(TableAccessor table, string name)
            : base(table, name) 
        {
            
        }

        public override void Execute(Migration migration)
        {
            migration.Database.RemoveConstraint(this.Table.TableName, this.Name);
        }
    }

    public class CreateUniqueConstraintAccessor : UniqueConstraintAccessor
    {
        public IList<ColumnAccessor> Columns { get; set; }

        public CreateUniqueConstraintAccessor(TableAccessor table, string name, IList<ColumnAccessor> columns)
            : base(table, name)
        {
            Name = name;
            Columns = columns;
        }

        public override void Execute(Migration migration)
        {
            migration.Database.AddUniqueConstraint(Name, Table.TableName,
                Columns.Select(x => x.Name).ToArray());
        }

    }

    public abstract class UniqueConstraintAccessor : InsideTableAccessor
    {
        public string Name { get; set; }

        public UniqueConstraintAccessor(TableAccessor table, string name) : base(table)
        {
            Name = name;
        }

        
    }
}
