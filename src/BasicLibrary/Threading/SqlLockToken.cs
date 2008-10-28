using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace BasicLibrary.Threading
{
    public abstract class SqlLockToken<T> : BaseLockToken, IDataStoreLockToken
    {
        public T Transaction { get; set; }
        public object Data { get; set; }

        public abstract void Commit();
        public abstract void BeginTransaction();

        public override bool ValidState
        {
            get { return Transaction != null; }
        }

        public SqlLockToken(T transaction, string type, int id)
            : base(type, id)
        {
            Transaction = transaction;
        }
       
    }
}
