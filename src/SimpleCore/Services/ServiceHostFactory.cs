using System;
using System.Collections.Generic;
using System.Text;
using Simple.ConfigSource;
using Simple.Reflection;
using Simple.Client;
using log4net;
using System.Reflection;
using Simple.Patterns;

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
            foreach (Type contract in GetContractsFromType(type))
            {
                ConfigCache.Host(type, contract);
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
            foreach (Type t in Enumerable.Filter(type.Assembly.GetTypes(), t => t.IsClass && !t.IsAbstract))
            {
                Host(t);
            }
        }

        #endregion
    }
}
