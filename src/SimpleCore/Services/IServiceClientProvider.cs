using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.DynamicProxy;

namespace Simple.Services
{
    public interface IServiceClientProvider
    {
        object Create(Type type);
    }

    public class NullServiceClientProvider : IServiceClientProvider
    {
        #region IServiceFactory Members

        public object Create(Type type)
        {
            return DynamicProxyFactory.Instance.CreateProxy(null, (o, m, p) =>
            {
                return null;
            });
        }

        #endregion
    }
}
