using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrator.Fluent
{
    public interface IConvention
    {
        string PrimaryKeyColumn { get; }
        string ForeignKeyConstraint(string fkTable, string fkColumn, string pkTable, string pkColumn, string tag);
    }

    public class DefaultConvention : IConvention
    {
        #region IConvention Members

        public string PrimaryKeyColumn
        {
            get { return "id"; }
        }
        
        public string ForeignKeyConstraint(string fkTable, string fkColumn, string pkTable, string pkColumn, string tag)
        {
            string res = fkTable + "_" + pkTable;
            if (!string.IsNullOrEmpty(tag)) res += "_" + tag;
            return res + "_fk";
        }

        #endregion
    }
}
