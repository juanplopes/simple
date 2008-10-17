using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace BasicLibrary.Threading
{
    public class SqlLockToken : BaseLockToken, IDataStoreLockToken
    {
        public SqlTransaction Transaction { get; set; }
        public object Data { get; set; }

        public override bool ValidState
        {
            get { return Transaction != null; }
        }

        public SqlLockToken(SqlTransaction transaction, string type, int id)
            : base(type, id)
        {
            Transaction = transaction;
        }
       
    }
}
