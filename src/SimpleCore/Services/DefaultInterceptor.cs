using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Reflection;
using log4net;


namespace Simple.Services
{
    public class DefaultInterceptor : IInterceptor
    {
        IDictionary<MethodBase, InvocationDelegate> _cache = new Dictionary<MethodBase, InvocationDelegate>();
        Func<CallHookArgs, IEnumerable<ICallHook>> Hooks { get; set; }
        bool Client { get; set; }
        ICallHeadersHandler HeaderHandler { get; set; }

        public DefaultInterceptor(Func<CallHookArgs, IEnumerable<ICallHook>> hooks, ICallHeadersHandler headerHandler, bool client)
        {
            Hooks = hooks;
            Client = client;
            HeaderHandler = headerHandler;
        }

        #region IInterceptor Members

        public virtual object Intercept(object target, MethodBase method, object[] args)
        {
            var hookArgs = new CallHookArgs(target, method, args, Client);
            var methodHooks = Hooks(hookArgs);

            ILog logger = Simply.Do.Log(this);

            var list = new List<ICallHook>(methodHooks);

            try
            {
                foreach (var hook in Enumerable.Reverse(list)) hook.Before();

                if (Client) HeaderHandler.InjectCallHeaders(target, method, args);
                else HeaderHandler.RecoverCallHeaders(target, method, args);

                hookArgs.Return = Invoke(target, method, args);

                logger.DebugFormat("Calling {0} in {1}...", method.Name, method.DeclaringType.Name);

                if (Client) HeaderHandler.RecoverCallHeaders(target, method, args);
                else HeaderHandler.InjectCallHeaders(target, method, args);

                logger.DebugFormat("Returning from {0} in {1}...", method.Name, method.DeclaringType.Name);


                foreach (var hook in list) hook.AfterSuccess();

                return hookArgs.Return;
            }
            finally
            {
                logger.DebugFormat("Finalizing {0} in {1}...", method.Name, method.DeclaringType.Name);

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
