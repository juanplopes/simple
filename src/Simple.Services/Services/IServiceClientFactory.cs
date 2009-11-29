using System;
namespace Simple.Services
{
    public interface IServiceClientFactory
    {
        object Resolve(Type type);
        T Resolve<T>();

        void ClearHooks();
        void AddHook(Func<CallHookArgs, ICallHook> hookCreator);
    }
}
