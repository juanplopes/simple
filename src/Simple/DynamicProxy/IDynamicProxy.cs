using System;
using System.Reflection;

namespace Simple.DynamicProxy
{
    /// <summary>
    /// Delegate for implementing the invocation task in a Dynamic Proxy
    /// <code>
    /// Example of an invocation handler
    /// DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler))
    /// 
    /// private static object InvocationHandler(object target, MethodBase method, object[] parameters) {
    ///		Console.WriteLine("Before: " + method.Name);
    ///		object result = method.Invoke(target, parameters);
    ///		Console.WriteLine("After: " + method.Name);
    ///		return result;
    ///	}
    /// </code>
    /// </summary>
    public delegate object InvocationDelegate(object target, MethodBase method, object[] parameters);

    /// <summary>
    /// Interface for a dynamic proxy. Through this interface you can work on the proxy instance.
    /// </summary>
    public interface IDynamicProxy
    {
        /// <summary>
        /// The target object for the proxy (aka. the proxied object)
        /// </summary>
        object ProxyTarget
        {
            get;
        }

        /// <summary>
        /// The delegate which handles the invocation task in the dynamic proxy
        /// </summary>
        InvocationDelegate InvocationHandler
        {
            get;
            set;
        }

        /// <summary>
        /// Type support strictness. Used for cast strictness
        /// </summary>
        bool Strict
        {
            get;
            set;
        }

        /// <summary>
        /// List of supported types for cast strictness support. Is only checked if Strict is true
        /// </summary>
        Type[] SupportedTypes
        {
            get;
            set;
        }
    }
}
