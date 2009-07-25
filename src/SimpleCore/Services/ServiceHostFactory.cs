using System;
using System.Collections.Generic;
using System.Text;
using Simple.ConfigSource;
using Simple.Reflection;
using log4net;
using System.Reflection;
using Simple.Patterns;
using Simple.DynamicProxy;

namespace Simple.Services
{
    public class ServiceHostFactory : Factory<IServiceHostProvider>, Simple.Services.IServiceHostFactory
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());

        protected override void OnConfig(IServiceHostProvider config)
        {
        }


        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceHostProvider();
        }

        public void Host(Type type)
        {
            Host(type, null);
        }

        public void Host(Type type, IInterceptor interceptor)
        {
            foreach (Type contract in GetContractsFromType(type))
            {
                object server = Activator.CreateInstance(type);
                if (interceptor != null)
                    server = DynamicProxyFactory.Instance.CreateProxy(server, interceptor.Intercept);
                
                ConfigCache.Host(server, contract);
            }
        }

        protected IEnumerable<Type> GetContractsFromType(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            return Enumerable.Filter(interfaces, t => typeof(IService).IsAssignableFrom(t));
        }

        #region IServiceHostFactory Members


        public void HostAssemblyOf(Type type)
        {
            HostAssemblyOf(type, null);
        }

        public void HostAssemblyOf(Type type, IInterceptor interceptor)
        {
            foreach (Type t in Enumerable.Filter(type.Assembly.GetTypes(), t => t.IsClass && !t.IsAbstract))
            {
                Host(t, interceptor);
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
    }
}
