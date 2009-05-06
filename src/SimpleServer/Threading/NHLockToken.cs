using System;
using System.Collections.Generic;

using System.Text;
using Simple.Threading;
using NHibernate;
using Simple.DataAccess;
using System.Data;

namespace Simple.Threading
{
    public class NHLockToken : SqlLockToken<ISession>
    {
        public override void Commit()
        {
            this.Transaction.Flush();
            this.Transaction.Transaction.Commit();
            this.Transaction.Close();
        }

        public override void BeginTransaction()
        {
            this.Transaction = SessionManager.GetSession(true);
            this.Transaction.BeginTransaction(IsolationLevel.Serializable);
        }

        public NHLockToken(string type, int id) : base(null, type, id)
        {
            BeginTransaction();
        }
    }
}
