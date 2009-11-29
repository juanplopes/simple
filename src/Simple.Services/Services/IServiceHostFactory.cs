using System;
using System.Reflection;
namespace Simple.Services
{
    public interface IServiceHostFactory
    {
        void StartServer();
        void Host(Type type);
        void HostAssembly(Assembly asm);
        void HostAssemblyOf(Type type);
        void ClearHosted();
        void AddHook(Func<CallHookArgs, ICallHook> hookCreator);
        void ClearHooks();
        void StopServer();
    }
}
