using System;
using System.Reflection;

namespace Simple.Services
{
    delegate object ServiceHostInterceptor(object target, MethodBase method, object[] args);

    public interface IServiceHostProvider : IServiceCommonProvider
    {
        void Host(object server, Type contract);
        void Start();
        void Stop();
    }

    public class NullServiceHostProvider : IServiceHostProvider
    {

        public void Host(object server, Type contract)
        {
        }

        public void Start()
        {
        }

        public void Stop()
        {

        }

        public object ProxyObject(object obj, Type type, IInterceptor interceptor)
        {
            return obj;
        }


        public ICallHeadersHandler HeaderHandler
        {
            get { return new NullCallHeadersHandler(); }
        }
    }
}
