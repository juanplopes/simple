using System.Collections.Generic;
using System.Reflection;
using Simple.Patterns;
using System;
using System.Linq;

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

        public T CreateInstance<T>(params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), parameters);
        }

        public T CreateInstance<T>(BindingFlags? flags, params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), flags, parameters);
        }

        public object CreateInstance(Type type, params object[] parameters)
        {
            return CreateInstance(type, null, parameters);
        }

        public object CreateInstance(Type type, BindingFlags? flags, params object[] parameters)
        {
            var method = FindConstructor(type, flags, ref parameters);

            return GetInvoker(method)(null, parameters);
        }

        public InvocationDelegate GetConstructor(Type type, params Type[] argTypes)
        {
            return GetInvoker(type.GetConstructor(argTypes));
        }

        private static ConstructorInfo FindConstructor(Type type, BindingFlags? flags, ref object[] parameters)
        {
            var ctors = flags != null ? type.GetConstructors(flags.Value) : type.GetConstructors();
            object state;
            var method = Type.DefaultBinder.BindToMethod(
                flags ?? BindingFlags.Default, ctors, ref parameters,
                null, null, null, out state) as ConstructorInfo;
            return method;
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
