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
            HostFactory().Host(type);
        }

        public static void Host(object key, Type type)
        {
            HostFactory(key).Host(type);
        }

        public static void Host(Type type, IInterceptor interceptor)
        {
            HostFactory().Host(type, interceptor);
        }

        public static void Host(object key, Type type, IInterceptor interceptor)
        {
            HostFactory(key).Host(type, interceptor);
        }

        public static void HostAssemblyOf(Type type)
        {
            HostFactory().HostAssemblyOf(type);
        }

        public static void HostAssemblyOf(object key, Type type)
        {
            HostFactory(key).HostAssemblyOf(type);
        }

        public static void HostAssemblyOf(Type type, IInterceptor interceptor)
        {
            HostFactory().HostAssemblyOf(type, interceptor);
        }

        public static void HostAssemblyOf(object key, Type type, IInterceptor interceptor)
        {
            HostFactory(key).HostAssemblyOf(type, interceptor);
        }


        public static T Connect<T>()
        {
            return ClientFactory().Resolve<T>();
        }

        public static T Connect<T>(object key)
        {
            return ClientFactory(key).Resolve<T>();
        }

        public static object Connect(object key, Type type)
        {
            return ClientFactory(key).Resolve(type);
        }


        public static void StartServer()
        {
            HostFactory().StartServer();
        }

        public static void StartServer(object key)
        {
            HostFactory(key).StartServer();
        }

        public static void StopServer()
        {
            HostFactory().StopServer();
        }

        public static void StopServer(object key)
        {
            HostFactory(key).StopServer();
        }
    }
}
