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

namespace Simple.DynamicProxy
{
	/// <summary>
	/// Factory for creating Dynamic proxy instances
	/// <code>
	/// TestClasses.SimpleClass testClass = new TestClasses.SimpleClass();
	/// TestClasses.ISimpleInterface testClassProxy = (TestClasses.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler));
	/// testClassProxy.Method1();
	/// </code>
	/// <see cref="IDynamicProxy"/>
	/// </summary>
	public class DynamicProxyFactory {
		private static DynamicProxyFactory self = new DynamicProxyFactory();

		private DynamicProxyFactory() {
		}

		/// <summary>
		/// Get the instance of the factory (singleton)
		/// </summary>
		public static DynamicProxyFactory Instance {
			get { return self; }
		}

		/// <summary>
		/// Create a proxy for the target object
		/// </summary>
		/// <param name="target">The object to create a proxy for</param>
		/// <param name="invocationHandler">The invocation handler for the proxy</param>
		/// <returns>The dynamic proxy instance</returns>
		public object CreateProxy(object target, InvocationDelegate invocationHandler) {
			return CreateProxy(target, invocationHandler, false, null);
		}

		/// <summary>
		/// Create a proxy for the target object
		/// </summary>
		/// <param name="target">The object to create a proxy for</param>
		/// <param name="invocationHandler">The invocation handler for the proxy</param>
		/// <param name="strict">Indicates if the cast support should be strict. If strict is true all casts are checked before being performed</param>
		/// <returns>The dynamic proxy instance</returns>
		public object CreateProxy(object target, InvocationDelegate invocationHandler, bool strict) {
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
		public object CreateProxy(object target, InvocationDelegate invocationHandler, bool strict, Type[] supportedTypes) {
			return new DynamicProxyImpl(target, invocationHandler, strict, supportedTypes).GetTransparentProxy();
		}
	}
}
