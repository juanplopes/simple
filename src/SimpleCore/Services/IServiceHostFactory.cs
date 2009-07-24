using System;
namespace Simple.Services
{
    public interface IServiceHostFactory
    {
        void StartServer();
        void Host(Type type);
        void Host(Type type, IInterceptor interceptor);
        void HostAssemblyOf(Type type);
        void HostAssemblyOf(Type type, IInterceptor interceptor);
        void StopServer();
    }
}
