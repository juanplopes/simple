using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.DynamicProxy;
using Simple.Config;

namespace Simple.Services.Remoting
{
    public abstract class RemotingBaseProvider : Factory<RemotingConfig>, IServiceCommonProvider
    {
        public virtual object ProxyObject(object obj, Type type, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateMarshallableProxy((MarshalByRefObject)obj, intercept.Intercept);
        }

        public virtual ICallHeadersHandler HeaderHandler
        {
            get { return new CallContextCallHeadersHandler(); }
        }

    }
}
