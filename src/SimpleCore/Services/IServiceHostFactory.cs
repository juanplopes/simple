using System;
namespace Simple.Services
{
    public interface IServiceHostFactory
    {
        void StartServer();
        void Host(Type type);
        void HostAssemblyOf(Type type);
        void ClearHooks();
        void AddHook(Func<CallHookArgs, ICallHook> hookCreator);
        void StopServer();
    }
}
