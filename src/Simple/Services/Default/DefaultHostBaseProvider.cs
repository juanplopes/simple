using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using Simple.DynamicProxy;

namespace Simple.Services.Default
{
    public abstract class DefaultHostBaseProvider : Factory<DefaultHostConfig>, IServiceCommonProvider
    {
        public virtual object ProxyObject(object obj, Type type, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateMarshallableProxy((MarshalByRefObject)obj, intercept.Intercept);
        }

        public virtual ICallHeadersHandler HeaderHandler
        {
            get { return new NullCallHeadersHandler(); }
        }
    }
}
