using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services
{
    public class ServiceManager
    {
        private static FactoryManager<ServiceClientFactory, IServiceClientProvider> clientFactory =
            new FactoryManager<ServiceClientFactory, IServiceClientProvider>(() => new ServiceClientFactory());

        private static FactoryManager<ServiceHostFactory, IServiceHostProvider> hostFactory =
                    new FactoryManager<ServiceHostFactory, IServiceHostProvider>(() => new ServiceHostFactory());

        private static ServiceClientFactory ClientFactory()
        {
            return clientFactory.SafeGet();
        }

        private static ServiceClientFactory ClientFactory(object key)
        {
            return clientFactory.SafeGet(key);
        }

        private static ServiceHostFactory HostFactory()
        {
            return hostFactory.SafeGet();
        }

        private static ServiceHostFactory HostFactory(object key)
        {
            return hostFactory.SafeGet(key);
        }

        public static void Host(Type type)
        {
            HostFactory().Add(type);
        }

        public static void Host(object key, Type type)
        {
            HostFactory(key).Add(type);
        }

        public static T Connect<T>()
        {
            return ClientFactory().Create<T>();
        }

        public static T Connect<T>(object key)
        {
            return ClientFactory(key).Create<T>();
        }
    }
}
