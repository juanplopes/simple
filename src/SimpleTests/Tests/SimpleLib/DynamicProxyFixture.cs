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
using NUnit.Framework;
using Simple.DynamicProxy;

namespace Simple.Tests.SimpleLib
{
	/// <summary>
	/// Unit test for the dynamic proxy
	/// </summary>
	[TestFixture]
	public class DynamicProxyFixture {
		public DynamicProxyFixture() {
		}

		[Test]
		public void TestSimpleProxy() {
			Sample.SimpleClass testClass = new Sample.SimpleClass();
			Sample.ISimpleInterface testClassProxy = (Sample.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler));
            Assert.IsNotNull(testClassProxy);
			
			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();
			
			Assert.AreEqual("Hello World!", testClassProxy.Method2());
			Assert.AreEqual(10000, testClassProxy.Method3());
			Assert.AreEqual(123456, testClassProxy.Method4(123456));

			int outValue = 1234;
			testClassProxy.Method5(3456, out outValue);
			Assert.AreEqual(3456, outValue);

			int refValue = 56748;
			testClassProxy.Method6(ref refValue);
			Assert.AreEqual(98765, refValue);

			// Test casting
			Sample.IImplemented implementedInterface = (Sample.IImplemented)testClassProxy;
			Assert.IsNotNull(implementedInterface);
			implementedInterface.ImplementedMethod();

			// Test IDynamicProxy test
			IDynamicProxy dynProxy = (IDynamicProxy)testClassProxy;
			Assert.IsNotNull(dynProxy);
		}

		[Test]
		[ExpectedException(typeof(TargetException))]
		public void TestInvalidCastWithoutStrict() {
			Sample.SimpleClass testClass = new Sample.SimpleClass();
			Sample.ISimpleInterface testClassProxy = (Sample.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler));
			Assert.IsNotNull(testClassProxy);

			// Test invalid cast
			Sample.INotImplemented notImplementedInterface = (Sample.INotImplemented)testClassProxy;
			notImplementedInterface.NotImplementedMethod();
		}

		[Test]
		public void TestStrictness() {
			Sample.SimpleClass testClass = new Sample.SimpleClass();
			Sample.ISimpleInterface testClassProxy = (Sample.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true);
			Assert.IsNotNull(testClassProxy);

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test casting
			Sample.IImplemented implementedInterface = (Sample.IImplemented)testClassProxy;
			implementedInterface.ImplementedMethod();
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void TestStrictnessWithInvalidCast() {
			Sample.SimpleClass testClass = new Sample.SimpleClass();
			Sample.ISimpleInterface testClassProxy = (Sample.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true);
			Assert.IsNotNull(testClassProxy);

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test invalid cast
			Sample.INotImplemented notImplementedInterface = null;
			notImplementedInterface = (Sample.INotImplemented)testClassProxy;
		}


		[Test]
		public void TestStrictnessWithSupportedList() {
			Sample.SimpleClass testClass = new Sample.SimpleClass();
			Sample.ISimpleInterface testClassProxy = (Sample.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true, new Type[] { typeof(Sample.INotImplemented) });
			Assert.IsNotNull(testClassProxy);

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test casting
			Sample.IImplemented implementedInterface = (Sample.IImplemented)testClassProxy;
			Assert.IsNotNull(implementedInterface);
			implementedInterface.ImplementedMethod();

			// Test invalid cast but which is supported via the supported type list
			Sample.INotImplemented notImplementedInterface = null;
			notImplementedInterface = (Sample.INotImplemented)testClassProxy;
			Assert.IsNotNull(notImplementedInterface);
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void TestStrictnessWithSupportedListAndInvalidCast() {
			Sample.SimpleClass testClass = new Sample.SimpleClass();
			Sample.ISimpleInterface testClassProxy = (Sample.ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true, new Type[] { typeof(Sample.INotImplemented) });
			Assert.IsNotNull(testClassProxy);

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test invalid cast
			Sample.INotImplemented2 notImplementedInterface2 = null;
			notImplementedInterface2 = (Sample.INotImplemented2)testClassProxy;
		}

		private object InvocationHandler(object target, MethodBase method, object[] parameters) {
			Console.WriteLine("Before: " + method.Name);
			object result = method.Invoke(target, parameters);
			Console.WriteLine("After: " + method.Name);
			return result;
		}

	}
}
