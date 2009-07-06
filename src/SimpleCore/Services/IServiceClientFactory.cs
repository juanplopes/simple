using System;
namespace Simple.Services
{
    public interface IServiceClientFactory
    {
        object Connect(Type type);
        T Connect<T>();
    }
}
