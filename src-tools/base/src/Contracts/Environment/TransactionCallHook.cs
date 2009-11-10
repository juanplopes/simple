using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;
using NHibernate;
using Simple;
using Simple.Reflection;

namespace Sample.Project.Environment
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresTransactionAttribute : Attribute
    {
    }

    public class TransactionCallHook : BaseCallHook
    {

        public TransactionCallHook(CallHookArgs args) : base(args) { }

        ITransaction tx = null;

        public override void Before()
        {
            if (this.CallArgs.Method.IsDefined(typeof(RequiresTransactionAttribute), true) &&
                Simply.Do.GetSession().Transaction == null)
                tx = Simply.Do.BeginTransaction();
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
