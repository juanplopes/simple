using System;
using Simple.DynamicProxy;

namespace Simple.Services
{
    public interface IServiceCommonProvider
    {
        object ProxyObject(object obj, Type contract, IInterceptor intercept);
        IContextHandler HeaderHandler { get; }
    }

    public interface IServiceClientProvider : IServiceCommonProvider
    {
        object Create(Type type);
    }

    public class NullServiceClientProvider : IServiceClientProvider
    {
        private object _cache = null;

        #region IServiceFactory Members

        public object Create(Type contract)
        {
            lock (this)
            {
                if (_cache == null)
                {
                    _cache = DynamicProxyFactory.Instance.CreateProxy(null, (o, m, p) =>
                    {
                        throw new NotImplementedException();
                    });
                }

                return _cache;
            }
        }

        public object ProxyObject(object obj, Type type, IInterceptor intercept)
        {
            return obj;
        }


        #endregion

        #region IServiceCommonProvider Members


        public IContextHandler HeaderHandler
        {
            get { return new NullContextHandler(); }
        }

        #endregion
    }
}
