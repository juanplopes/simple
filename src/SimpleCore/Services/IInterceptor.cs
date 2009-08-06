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

    public class DefaultInterceptor : IInterceptor
    {
        IDictionary<MethodBase, InvocationDelegate> _cache = new Dictionary<MethodBase, InvocationDelegate>();
        Func<CallHookArgs, IEnumerable<ICallHook>> Hooks { get; set; }

        public DefaultInterceptor(Func<CallHookArgs, IEnumerable<ICallHook>> hooks)
        {
            Hooks = hooks;
        }

        #region IInterceptor Members

        public virtual object Intercept(object target, MethodBase method, object[] args)
        {
            var hookArgs = new CallHookArgs(target, method, args);
            var methodHooks = Hooks(hookArgs);

            var list = new List<ICallHook>(methodHooks);

            try
            {
                foreach (var hook in Enumerable.Reverse(list)) hook.Before();

                hookArgs.Return = Invoke(target, method, args);

                foreach (var hook in list) hook.AfterSuccess();

                return hookArgs.Return;
            }
            finally
            {
                foreach (var hook in list) hook.Finally();
            }
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
