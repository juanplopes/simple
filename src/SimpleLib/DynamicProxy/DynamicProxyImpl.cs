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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Diagnostics;

namespace Simple.DynamicProxy
{
	/// <summary>
	/// The implementation for a dynamic proxy. Should not be instantiated directly, but rather through the
	/// DynamicProxyFactory
	/// </summary>
	public class DynamicProxyImpl : RealProxy, IDynamicProxy, IRemotingTypeInfo {
		/// <summary>
		/// The object we are the proxy for
		/// </summary>
		private object proxyTarget;
		/// <summary>
		/// Should we be strict regarding interface support?
		/// </summary>
		private bool strict;
		/// <summary>
		/// A list of the types we support. Is only used when strict is true. The proxy targets type(s) are automatically supported
		/// </summary>
		private Type[] supportedTypes;
		/// <summary>
		/// The delegate for handling the invocation part of the method call process
		/// </summary>
		private InvocationDelegate invocationHandler;

		/// <summary>
		/// Creates a new proxy instance, with proxyTarget as the proxied object
		/// </summary>
		/// <param name="proxyTarget">The object to proxy</param>
		/// <param name="strict">Should type support (for casts) be strict or loose</param>
		/// <param name="supportedTypes">A List of supported types. Only used if strict is true. May be null</param>
		protected internal DynamicProxyImpl(object proxyTarget, InvocationDelegate invocationHandler, bool strict, Type[] supportedTypes) : base(typeof(IDynamicProxy)) {
			this.proxyTarget = proxyTarget;
			this.invocationHandler = invocationHandler;
			this.strict = strict;
			this.supportedTypes = supportedTypes;
		}

		/// <summary>
		/// CreateObjRef() isn't supported.
		/// </summary>
		/// <param name="requestedType"></param>
		/// <returns>Nothing</returns>
		/// <exception cref="NotSupportedException">CreateObjRef() for DynamicProxy isn't supported</exception>
		public override ObjRef CreateObjRef(System.Type type) {
			throw new NotSupportedException("ObjRef for DynamicProxy isn't supported");
		}

		/// <summary>
		/// Checks whether the proxy representing the specified object type can be cast to the type represented by the IRemotingTypeInfo interface
		/// </summary>
		/// <param name="toType">The Type we wish to cast to</param>
		/// <param name="obj">The object we wish to cast</param>
		/// <returns>True if the strict property is false, otherwise the list of supportedTypes is checked.<br>
		/// The proxy targets type(s) are automatically supported</br></returns>
		public bool CanCastTo(System.Type toType, object obj) {
			// Assume we can (which is the default unless strict is true)
			bool canCast = true;

			if (strict) {
				// First check if the proxyTarget supports the cast
				if (toType.IsAssignableFrom(proxyTarget.GetType())) {
					canCast = true;
				} else if (supportedTypes != null) {
					canCast = false;
					// Check if the list of supported interfaces supports the cast
					foreach(Type type in supportedTypes) {
						if (toType == type) {
							canCast = true;
							break;
						}
					}
				} else {
					canCast = false;
				}
			}

			return canCast;
		}

		/// <summary>
		/// TypeName isn't supported since DynamicProxy doesn't support CreateObjRef()
		/// </summary>
		/// <exception cref="NotSupported">TypeName for Dynamic Proxy isn't supported</exception>
		public string TypeName {
			get { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
			set { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
		}

		/// <summary>
		/// The reflective method for invoking methods. See documentation for RealProxy.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
        /// 
        //[DebuggerHidden]
		public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage message) {
			// Convert to a MethodCallMessage
			System.Runtime.Remoting.Messaging.IMethodCallMessage methodMessage = new System.Runtime.Remoting.Messaging.MethodCallMessageWrapper((System.Runtime.Remoting.Messaging.IMethodCallMessage)message);

			// Extract the method being called
			System.Reflection.MethodBase method = methodMessage.MethodBase;

			// Perform the call
			object returnValue = null;
			if (method.DeclaringType == typeof(IDynamicProxy)) {
				// Handle IDynamicProxy interface calls on this instance instead of on the proxy target instance
				returnValue = method.Invoke(this, methodMessage.Args);
			} else {
				// Delegate to the invocation handler
				returnValue = invocationHandler(proxyTarget, method, methodMessage.Args);
			}
		
			// Create the return message (ReturnMessage)
			System.Runtime.Remoting.Messaging.ReturnMessage returnMessage = new System.Runtime.Remoting.Messaging.ReturnMessage(returnValue, methodMessage.Args, methodMessage.ArgCount, methodMessage.LogicalCallContext, methodMessage);
			return returnMessage;
		}

		/// <summary>
		/// Returns the target object for the proxy
		/// </summary>
		public object ProxyTarget {
			get { return proxyTarget; }
			set { proxyTarget = value; }
		}

		/// <summary>
		/// The delegate which handles the invocation task in the dynamic proxy
		/// </summary>
		public InvocationDelegate InvocationHandler {
			get { return invocationHandler; }
			set { invocationHandler = value; }
		}
		/// <summary>
		/// Type support strictness. Used for cast strictness
		/// </summary>
		public bool Strict {
			get { return strict; }
			set { strict = value; }
		}

		/// <summary>
		/// List of supported types for cast strictness support. Is only checked if Strict is true
		/// </summary>
		public Type[] SupportedTypes {
			get { return supportedTypes; }
			set { supportedTypes = value; }
		}

	}
}
