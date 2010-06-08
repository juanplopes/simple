using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Migrator.Fluent
{
    public interface IConvention
    {
        string PrimaryKeyColumn(string table);
        string ForeignKeyConstraint(string fkTable, string fkColumn, string pkTable, string pkColumn, string tag);
    }

    public class DefaultConvention : IConvention
    {
        #region IConvention Members

        public string PrimaryKeyColumn(string table)
        {
            return "id";
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
