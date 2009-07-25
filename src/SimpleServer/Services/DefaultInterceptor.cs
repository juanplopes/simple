using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Simple.DataAccess.Context;

namespace Simple.Services
{
    public class DefaultInterceptor : BaseInterceptor
    {
        public object ConfigKey { get; set; }
        public DefaultInterceptor(object key)
        {
            this.ConfigKey = key;
        }

        public override object Intercept(object target, System.Reflection.MethodBase method, object[] args)
        {
            using (IDataContext dx = Simply.Get(ConfigKey).EnterContext())
            {
                return base.Intercept(target, method, args);
            }
        }
    }
}
