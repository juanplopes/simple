using System.Collections.Generic;
using System.Reflection;
using Simple.Patterns;
using System;

namespace Simple.Reflection
{
    public class MethodCache : Singleton<MethodCache>
    {
        Dictionary<MethodBase, InvocationDelegate> _methods = new Dictionary<MethodBase, InvocationDelegate>();

        public InvocationDelegate GetInvoker(MethodBase method)
        {
            lock (_methods)
            {
                InvocationDelegate res;

                if (!_methods.TryGetValue(method, out res))
                    _methods[method] = res = InvokerFactory.Do.Create(method);

                return res;
            }
        }

        public object CreateInstance(Type type, params object[] parameters)
        {
            return GetInvoker(type.GetConstructor(Type.GetTypeArray(parameters)))(null, parameters);
        }

        public InvocationDelegate GetGetter(PropertyInfo prop)
        {
            return GetInvoker(prop.GetGetMethod());
        }

        public InvocationDelegate GetSetter(PropertyInfo prop)
        {
            return GetInvoker(prop.GetSetMethod());
        }
    }
}
