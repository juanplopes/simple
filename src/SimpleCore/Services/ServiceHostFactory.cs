using System;
using System.Collections.Generic;
using System.Text;
using Simple.ConfigSource;
using Simple.Reflection;
using log4net;
using System.Reflection;
using Simple.Patterns;
using Simple.DynamicProxy;
using Castle.DynamicProxy;

namespace Simple.Services
{
    public class ServiceHostFactory : Factory<IServiceHostProvider>, Simple.Services.IServiceHostFactory
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        protected IList<Func<CallHookArgs, ICallHook>> CallHookCreators { get; set; }

        protected override void OnConfig(IServiceHostProvider config)
        {
            ClearHooks();
        }


        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceHostProvider();
            ClearHooks();
        }

        public void Host(Type type)
        {
            foreach (Type contract in GetContractsFromType(type))
            {
                object server = Activator.CreateInstance(type);
                server = ProxyObject(server, new DefaultInterceptor(GetHooks, ConfigCache.HeaderHandler, false), contract);
                ConfigCache.Host(server, contract);
            }
        }

        protected IEnumerable<ICallHook> GetHooks(CallHookArgs args)
        {
            foreach (var hook in Enumerable.Convert(CallHookCreators, x => x(args)))
            {
                if (hook != null) yield return hook;
            }
        }

        protected object ProxyObject(object target, IInterceptor interceptor, Type contract)
        {
            return ConfigCache.ProxyObject(target, interceptor);
        }

        protected IEnumerable<Type> GetContractsFromType(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            return Enumerable.Filter(interfaces, 
                t => typeof(IService).IsAssignableFrom(t) && !typeof(IService).Equals(t));
        }

        #region IServiceHostFactory Members

        public void HostAssemblyOf(Type type)
        {
            foreach (Type t in Enumerable.Filter(type.Assembly.GetTypes(), t => t.IsClass && !t.IsAbstract))
            {
                Host(t);
            }
        }

        public void StartServer()
        {
            ConfigCache.Start();
        }

        public void StopServer()
        {
            ConfigCache.Stop();
        }

        #endregion

        #region IServiceHostFactory Members


        public void AddHook(Func<CallHookArgs, ICallHook> hookCreator)
        {
            CallHookCreators.Add(hookCreator);
        }

        public void ClearHooks()
        {
            CallHookCreators = new List<Func<CallHookArgs, ICallHook>>();
        }

        #endregion
    }
}
