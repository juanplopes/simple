using System;
using System.Reflection;
using Simple.Config;
using Simple.Patterns;
using Simple.Services;

namespace Simple
{
    public static class ServiceSimplyExtensions
    {
        private static FactoryManager<ServiceClientFactory, IServiceClientProvider> clientFactory =
            new FactoryManager<ServiceClientFactory, IServiceClientProvider>(() => new ServiceClientFactory());

        private static FactoryManager<ServiceHostFactory, IServiceHostProvider> hostFactory =
                    new FactoryManager<ServiceHostFactory, IServiceHostProvider>(() => new ServiceHostFactory());

        private static ServiceClientFactory ClientFactory(Simply simply)
        {
            return clientFactory.SafeGet(simply.ConfigKey);
        }

        private static ServiceHostFactory HostFactory(Simply simply)
        {
            return hostFactory.SafeGet(simply.ConfigKey);
        }


        public static void Host(this Simply simply, Type type)
        {
            HostFactory(simply).Host(type);
        }

        public static void HostAssembly(this Simply simply, Assembly asm)
        {
            HostFactory(simply).HostAssembly(asm);
        }

        public static void HostAssemblyOf(this Simply simply, Type type)
        {
            HostFactory(simply).HostAssemblyOf(type);
        }

        public static void ClearHostedServices(this Simply simply)
        {
            HostFactory(simply).ClearHosted();
        }

        public static void ClearServerHooks(this Simply simply)
        {
            HostFactory(simply).ClearHooks();
        }

        public static void AddServerHook(this Simply simply, Func<CallHookArgs, ICallHook> hookCreator)
        {
            HostFactory(simply).AddHook(hookCreator);
        }

        public static void ClearClientHooks(this Simply simply)
        {
            ClientFactory(simply).ClearHooks();
        }

        public static void AddClientHook(this Simply simply, Func<CallHookArgs, ICallHook> hookCreator)
        {
            ClientFactory(simply).AddHook(hookCreator);
        }

        public static T Resolve<T>(this Simply simply)
        {
            return ClientFactory(simply).Resolve<T>();
        }

        public static object Resolve(this Simply simply, Type type)
        {
            return ClientFactory(simply).Resolve(type);
        }

        public static DisposableAction EnterServiceMockContext(this Simply simply, Type type, object server)
        {
            return ClientFactory(simply).BeginServiceMockContext(type, server);
        }

        public static DisposableAction EnterServiceMockContext<T>(this Simply simply, T server)
        {
            return simply.EnterServiceMockContext(typeof(T), server);
        }

        public static void StartServer(this Simply simply)
        {
            HostFactory(simply).StartServer();
        }

        public static void StopServer(this Simply simply)
        {
            HostFactory(simply).StopServer();
        }
    }
}
