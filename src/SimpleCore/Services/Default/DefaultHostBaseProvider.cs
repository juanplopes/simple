using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.DynamicProxy;

namespace Simple.Services.Default
{
    public abstract class DefaultHostBaseProvider : Factory<DefaultHostConfig>, IServiceCommonProvider
    {
        public object ProxyObject(object obj, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateProxy(obj, intercept.Intercept);
        }

        public ICallHeadersHandler HeaderHandler
        {
            get { return new NullCallHeadersHandler(); }
        }
    }
}
