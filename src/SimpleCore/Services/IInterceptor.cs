using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Reflection;

namespace Simple.Services
{
    public interface IInterceptor
    {
        object Intercept(object target, MethodBase method, object[] args);
    }

    public class BaseInterceptor : IInterceptor
    {
        IDictionary<MethodBase, InvocationDelegate> _cache = new Dictionary<MethodBase, InvocationDelegate>();

        #region IInterceptor Members

        public virtual object Intercept(object target, MethodBase method, object[] args)
        {
            return Invoke(target, method, args);
        }

        protected object Invoke(object target, MethodBase method, object[] args)
        {
            return GetInvocationDelegate(method).Invoke(target, args);
        }

        protected InvocationDelegate GetInvocationDelegate(MethodBase method)
        {
            lock (_cache)
            {
                InvocationDelegate del;
                if (!_cache.TryGetValue(method, out del))
                {
                    _cache[method] = del = InvokerFactory.Do.Create(method);
                }
                return del;

            }
        }

        #endregion
    }
}
