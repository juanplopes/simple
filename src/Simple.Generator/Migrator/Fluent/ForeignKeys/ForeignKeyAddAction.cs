using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public class ForeignKeyAddAction : ForeignKeyAction
    {
        public ForeignKeyAddAction(TableAction table, string name, string foreignKeyColumn, string primaryKeyTable, string primaryKeyColumn)
            : base(table, name, foreignKeyColumn, primaryKeyTable, primaryKeyColumn) { }

        public ForeignKeyAddAction(TableAction table, string name, IList<string> foreignKeyColumns, string primaryKeyTable, IList<string> primaryKeyColumns)
            : base(table, name, foreignKeyColumns, primaryKeyTable, primaryKeyColumns) { }


        public ForeignKeyConstraint? Constraint { get; set; }

        public ForeignKeyAddAction OnConflict(ForeignKeyConstraint constraint)
        {
            this.Constraint = constraint;
            return this;
        }

        public ForeignKeyAddAction OnConflictCascade()
        {
            return OnConflict(ForeignKeyConstraint.Cascade);
        }

        public ForeignKeyAddAction OnConflictRestrict()
        {
            return OnConflict(ForeignKeyConstraint.Restrict);
        }

        public ForeignKeyAddAction OnConflictSetDefault()
        {
            return OnConflict(ForeignKeyConstraint.SetDefault);
        }

        public ForeignKeyAddAction OnConflictSetNull()
        {
            return OnConflict(ForeignKeyConstraint.SetNull);
        }

        public ForeignKeyAddAction OnConflictNoAction()
        {
            return OnConflict(ForeignKeyConstraint.NoAction);
        }


        public override void Execute(ITransformationProvider provider)
        {
            provider.AddForeignKey(this.Name, Table.Name, this.FkColumns.ToArray(), this.PkTable, this.PkColumns.ToArray(), 
                Constraint ?? ForeignKeyConstraint.NoAction);
        }
    }
}
