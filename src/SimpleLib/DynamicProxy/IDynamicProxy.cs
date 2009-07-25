/*
 * DynamicProxy.NET
 * (C) Copyright 2003 Jeppe Cramon (jeppe@cramon.dk)
 * http://www.cramon.dk
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * Disclaimer:
 * -----------
 * This software is provided "as is" without warranty of any kind,
 * either expressed or implied. The entire risk as to the
 * quality and performance of the software is with you. Should the
 * software prove defective, you assume the cost of all necessary
 * servicing, repair, or correction. In no event shall the author,
 * copyright holder, or any other party who may redistribute the
 * software be liable to you for damages, including any general,
 * special, incidental, or consequential damages arising out of
 * the use or inability to use the software (including, but not
 * limited to, loss of data, data being rendered inaccurate, loss of
 * business profits, loss of business information, business
 * interruptions, loss sustained by you or third parties, or a
 * failure of the software to operate with any other software) even
 * if the author, copyright holder, or other party has been advised
 * of the possibility of such damages. 
 * 
 */
using System;
using System.Reflection;
using System.Runtime.Remoting;

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
