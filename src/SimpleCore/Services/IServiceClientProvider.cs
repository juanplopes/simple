using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.DynamicProxy;
using System.Reflection;

namespace Simple.Services
{
    public interface IServiceClientProvider
    {
        object Create(Type type);
    }

    public class NullServiceClientProvider : IServiceClientProvider
    {
        private object _cache = null;

        #region IServiceFactory Members

        public object Create(Type type)
        {
            lock (this)
            {
                if (_cache == null)
                {
                    _cache = DynamicProxyFactory.Instance.CreateProxy(null, (o, m, p) =>
                    {
                        Type retType = ((MethodInfo)m).ReturnType;
                        return retType.IsValueType ? Activator.CreateInstance(retType) : null;
                    });
                }

                return _cache;
            }
        }

        #endregion
    }
}
