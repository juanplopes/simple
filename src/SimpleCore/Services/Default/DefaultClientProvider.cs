using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using Simple.DynamicProxy;

namespace Simple.Services.Default
{
    public class DefaultClientProvider : DefaultHostBaseProvider, IServiceClientProvider
    {
        #region IServiceClientProvider Members

        public object Create(Type type)
        {
            return ServiceLocationFactory.Do[ConfigCache].Get(type);
        }

        #endregion

        protected override void OnConfig(DefaultHostConfig config)
        {
        }

        protected override void OnClearConfig()
        {
        }

        #region IServiceClientProvider Members


        public override object ProxyObject(object obj, Type type, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateProxy(obj, intercept.Intercept);
        }

        #endregion
    }
}
