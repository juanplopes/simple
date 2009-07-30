using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Simple.DataAccess.Context;

namespace Simple.Services
{
    public class DefaultCallHook : BaseCallHook
    {
        public object ConfigKey { get; protected set; }
        public IDataContext Context { get; protected set; }
        public DefaultCallHook(CallHookArgs args, object key)
            : base(args)
        {
            this.ConfigKey = key;
        }

        public override void Before()
        {
            Context = DataContextFactory.Get(ConfigKey).EnterContext();
        }

        public override void AfterSuccess()
        {
        }

        public override void Finally()
        {
            Context.Dispose();
        }
    }
}
