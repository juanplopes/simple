using System;
using Simple.Config;
using Simple.DynamicProxy;

namespace Simple.Services.Remoting
{
    public abstract class RemotingBaseProvider : Factory<RemotingConfig>, IServiceCommonProvider
    {
        public virtual object ProxyObject(object obj, Type type, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateMarshallableProxy((MarshalByRefObject)obj, intercept.Intercept);
        }

        public virtual IContextHandler HeaderHandler
        {
            get { return new CallContextHandler(); }
        }

    }
}
