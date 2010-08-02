using System;

namespace Simple.DynamicProxy
{
    public class DynamicProxyFactory
    {
        private static DynamicProxyFactory self = new DynamicProxyFactory();

        private DynamicProxyFactory()
        {
        }

        /// <summary>
        /// Get the instance of the factory (singleton)
        /// </summary>
        public static DynamicProxyFactory Instance
        {
            get { return self; }
        }

        /// <summary>
        /// Create a proxy for the target object
        /// </summary>
        /// <param name="target">The object to create a proxy for</param>
        /// <param name="invocationHandler">The invocation handler for the proxy</param>
        /// <returns>The dynamic proxy instance</returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler)
        {
            return CreateProxy(target, invocationHandler, false, null);
        }

        /// <summary>
        /// Create a proxy for the target object
        /// </summary>
        /// <param name="target">The object to create a proxy for</param>
        /// <param name="invocationHandler">The invocation handler for the proxy</param>
        /// <param name="strict">Indicates if the cast support should be strict. If strict is true all casts are checked before being performed</param>
        /// <returns>The dynamic proxy instance</returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler, bool strict)
        {
            return CreateProxy(target, invocationHandler, strict, null);
        }

        /// <summary>
        /// Create a proxy for the target object
        /// </summary>
        /// <param name="target">The object to create a proxy for</param>
        /// <param name="invocationHandler">The invocation handler for the proxy</param>
        /// <param name="strict">Indicates if the cast support should be strict. If strict is true all casts are checked before being performed. The supportedType list will enabled support for more interfaces than the target object supports</param>
        /// <param name="supportedTypes">List of types that are supported for casts. Is only checked if strict is true.</param>
        /// <returns>The dynamic proxy instance</returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler, bool strict, Type[] supportedTypes)
        {
            return new InterfaceDynamicProxyImpl(target, invocationHandler, strict, supportedTypes).GetTransparentProxy();
        }

        public object CreateMarshallableProxy(Type type, MarshalByRefObject target, InvocationDelegate invocationHandler)
        {
            return new MarshallableDynamicProxyImpl(type, target, invocationHandler).GetTransparentProxy();
        }


        public object CreateMarshallableProxy(MarshalByRefObject target, InvocationDelegate invocationHandler)
        {
            return new MarshallableDynamicProxyImpl(target, invocationHandler).GetTransparentProxy();
        }
    }
}
