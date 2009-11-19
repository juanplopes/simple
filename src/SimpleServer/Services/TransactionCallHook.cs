using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Simple.Services
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresTransactionAttribute : Attribute
    {
    }

    public class TransactionCallHook : BaseCallHook
    {
        public object ConfigKey { get; protected set; }

        public TransactionCallHook(CallHookArgs args)
            : this(args, null) { }


        public TransactionCallHook(CallHookArgs args, object key)
            : base(args)
        {
            ConfigKey = key;
        }

        ITransaction tx = null;

        public override void Before()
        {

            if (this.CallArgs.Method.IsDefined(typeof(RequiresTransactionAttribute), true))
            {
                var currTx = Simply.Do[ConfigKey].GetSession().Transaction;
                if (currTx == null || !currTx.IsActive || currTx.WasCommitted || currTx.WasRolledBack)
                    tx = Simply.Do[ConfigKey].BeginTransaction();
            }
        }

        public override void AfterSuccess()
        {
            if (tx != null) tx.Commit();
        }

        public override void Finally()
        {
            if (tx != null)
                if (!tx.WasCommitted) tx.Rollback();
        }
    }
}
