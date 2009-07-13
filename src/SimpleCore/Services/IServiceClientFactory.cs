using System;
namespace Simple.Services
{
    public interface IServiceClientFactory
    {
        object Resolve(Type type);
        T Resolve<T>();
    }
}
