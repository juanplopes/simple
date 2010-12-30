using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Simple.Reflection;


namespace Simple.Services
{
    public class DefaultInterceptor : IInterceptor
    {
        static MethodCache _cache = new MethodCache();
        static InterfaceMapCache _ifaceCache = new InterfaceMapCache();
        static ILog logger = Simply.Do.Log(MethodBase.GetCurrentMethod());

        Func<CallHookArgs, IEnumerable<ICallHook>> Hooks { get; set; }
        bool Client { get; set; }
        IContextHandler HeaderHandler { get; set; }

        public DefaultInterceptor(Func<CallHookArgs, IEnumerable<ICallHook>> hooks, IContextHandler headerHandler, bool client)
        {
            Hooks = hooks;
            Client = client;
            HeaderHandler = headerHandler;
        }

        #region IInterceptor Members

        protected MethodBase CorrectMethod(object target, MethodBase method)
        {
            if (target == null) throw new ArgumentException("server cannot be null");
            Type targetType = target.GetType();
            Type declaringType = method.DeclaringType;
            if (targetType != declaringType && declaringType.IsInterface)
            {
                var map = _ifaceCache.GetMap(targetType, method.DeclaringType);
                for (int i = 0; i < map.InterfaceMethods.Length; i++)
                    if (map.InterfaceMethods[i] == method) return map.TargetMethods[i];
            }
            return method;
        }

        public virtual object Intercept(object target, MethodBase method, object[] args)
        {
            if (!Client) method = CorrectMethod(target, method);
            var hookArgs = new CallHookArgs(target, method, args, Client);
            var list = Hooks(hookArgs).ToList();

            try
            {
                BeforeExecuteActions(target, method, args, list);
                hookArgs.Return = Invoke(target, method, args);
                AfterExecuteActions(target, method, args, list);

                return hookArgs.Return;
            }
            finally
            {
                FinallyExecuteActions(method, list);
            }
        }

        private void FinallyExecuteActions(MethodBase method, List<ICallHook> list)
        {
            logger.DebugFormat("Finalizing {0} in {1}...", method.Name, method.DeclaringType.Name);

            foreach (var hook in list) hook.Finally();
        }

        private void AfterExecuteActions(object target, MethodBase method, object[] args, List<ICallHook> list)
        {
            if (Client) HeaderHandler.RecoverCallHeaders(target, method, args);
            else HeaderHandler.InjectCallHeaders(target, method, args);

            logger.DebugFormat("Returning from {0} in {1}...", method.Name, method.DeclaringType.Name);

            foreach (var hook in list) hook.AfterSuccess();
        }

        private void BeforeExecuteActions(object target, MethodBase method, object[] args, List<ICallHook> list)
        {
            foreach (var hook in Enumerable.Reverse(list)) hook.Before();

            logger.DebugFormat("Calling {0} in {1}...", method.Name, method.DeclaringType.Name);

            if (Client) HeaderHandler.InjectCallHeaders(target, method, args);
            else HeaderHandler.RecoverCallHeaders(target, method, args);
        }

        protected object Invoke(object target, MethodBase method, object[] args)
        {
            return GetInvocationDelegate(method).Invoke(target, args);
        }

        protected InvocationDelegate GetInvocationDelegate(MethodBase method)
        {
            return _cache.GetInvoker(method);
        }

        #endregion
    }
}
