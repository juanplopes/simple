using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.DynamicProxy;

namespace Simple.Services.Default
{
    public class DefaultClientProvider : DefaultHostBaseProvider, IServiceClientProvider
    {
        #region IServiceClientProvider Members

        public object Create(Type type)
        {
            return ServiceLocationFactory.Get(ConfigCache).Get(type);
        }

        #endregion

        protected override void OnConfig(DefaultHostConfig config)
        {
        }

        protected override void OnClearConfig()
        {
        }

        #region IServiceClientProvider Members


        public object ProxyObject(object obj, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateProxy(obj, intercept.Intercept);
        }

        #endregion
    }
}
