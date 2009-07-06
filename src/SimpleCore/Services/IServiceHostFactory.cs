using System;
namespace Simple.Services
{
    public interface IServiceHostFactory
    {
        void Host(Type type);
    }
}
