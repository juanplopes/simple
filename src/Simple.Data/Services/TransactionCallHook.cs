using System;
using NHibernate;
using Simple.Reflection;

namespace Simple.Services
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresTransactionAttribute : Attribute
    {
    }

    public class TransactionCallHook : BaseCallHook
    {
        public object ConfigKey { get; protected set; }

        public TransactionCallHook(CallHookArgs args, object key)
            : base(args)
        {
            ConfigKey = key;
        }

        ITransaction tx = null;
        ISession session = null;

        public override void Before()
        {

            if (this.CallArgs.Method.IsDefined(typeof(RequiresTransactionAttribute), true))
            {
                session = Simply.Do[ConfigKey].GetSession();
                if (session != null)
                {
                    var currTx = session.Transaction;
                    if (currTx == null || !currTx.IsActive)
                        tx = Simply.Do[ConfigKey].BeginTransaction();
                }
            }
        }

        public override void AfterSuccess()
        {
            if (tx != null) tx.Commit();
        }

        public override void Finally()
        {
            if (tx != null)
            {
                session.Clear();
                if (!tx.WasCommitted) tx.Rollback();
            }
        }
    }
}
