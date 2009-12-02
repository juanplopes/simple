using System;
using System.Collections.Generic;
using System.Text;
using Simple.Config;
using Simple.Reflection;
using log4net;
using System.Linq;
using System.Reflection;
using Simple.Patterns;
using Simple.DynamicProxy;
using Castle.DynamicProxy;

namespace Simple.Services
{
    public class ServiceHostFactory : Factory<IServiceHostProvider>, Simple.Services.IServiceHostFactory
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        protected IList<Func<CallHookArgs, ICallHook>> CallHookCreators = new List<Func<CallHookArgs, ICallHook>>();
        protected HashSet<Type> Services = new HashSet<Type>();

        protected override void OnConfig(IServiceHostProvider config)
        {
            foreach (Type type in Services)
            {
                Host(type);
            }
        }
        
        protected override void OnDisposeOldConfig()
        {
            this.StopServer();
            base.OnDisposeOldConfig();
        }


        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceHostProvider();
        }

        public void Host(Type type)
        {
            Services.Add(type);
            foreach (Type contract in GetContractsFromType(type))
            {
                object server = Activator.CreateInstance(type);
                server = ProxyObject(server, new DefaultInterceptor(GetHooks, ConfigCache.HeaderHandler, false), contract);
                ConfigCache.Host(server, contract);
            }
        }

        protected IEnumerable<ICallHook> GetHooks(CallHookArgs args)
        {
            foreach (var hook in CallHookCreators.Select(x=>x(args)))
            {
                if (hook != null) yield return hook;
            }
        }

        protected object ProxyObject(object target, IInterceptor interceptor, Type contract)
        {
            return ConfigCache.ProxyObject(target, contract, interceptor);
        }

        protected IEnumerable<Type> GetContractsFromType(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            return interfaces.Where(x=>
                typeof(IService).IsAssignableFrom(x) && !typeof(IService).Equals(x));
        }

        #region IServiceHostFactory Members

        public void HostAssembly(Assembly asm)
        {
            foreach (Type t in asm.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
            {
                Host(t);
            }

        }

        public void HostAssemblyOf(Type type)
        {
            HostAssembly(type.Assembly);
        }

        public void StartServer()
        {
            ConfigCache.Start();
        }

        public void StopServer()
        {
            ConfigCache.Stop();
        }

        public void ClearHosted()
        {
            Services.Clear();
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
