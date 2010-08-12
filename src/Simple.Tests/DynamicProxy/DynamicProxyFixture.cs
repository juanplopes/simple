using System;
using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;
using Simple.DynamicProxy;

namespace Simple.Tests.DynamicProxy
{
	/// <summary>
	/// Unit test for the dynamic proxy
	/// </summary>
	[TestFixture]
	public class DynamicProxyFixture {
		public DynamicProxyFixture() {
		}

        protected static void TestCreatedSimpleProxy(ISimpleInterface testClassProxy)
        {
            testClassProxy.Should().Not.Be.Null();

            // No test for method 1, just make sure it doesn't bomb ;-)
            testClassProxy.Method1();

            testClassProxy.Method2().Should().Be("Hello World!");
            testClassProxy.Method3().Should().Be(10000);
            testClassProxy.Method4(123456).Should().Be(123456);

            int outValue = 1234;
            testClassProxy.Method5(3456, out outValue);
            outValue.Should().Be(3456);

            int refValue = 56748;
            testClassProxy.Method6(ref refValue);
            refValue.Should().Be(98765);

            // Test casting
            IImplemented implementedInterface = (IImplemented)testClassProxy;
            implementedInterface.Should().Not.Be.Null();
            implementedInterface.ImplementedMethod();

            // Test IDynamicProxy test
            IDynamicProxy dynProxy = (IDynamicProxy)testClassProxy;
            dynProxy.Should().Not.Be.Null();
        }

		[Test]
		public void TestSimpleProxy() {
			SimpleClass testClass = new SimpleClass();
			ISimpleInterface testClassProxy = (ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler));
            TestCreatedSimpleProxy(testClassProxy);
		}

        [Test]
		[ExpectedException(typeof(TargetException))]
		public void TestInvalidCastWithoutStrict() {
			SimpleClass testClass = new SimpleClass();
			ISimpleInterface testClassProxy = (ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler));
			testClassProxy.Should().Not.Be.Null();

			// Test invalid cast
			INotImplemented notImplementedInterface = (INotImplemented)testClassProxy;
			notImplementedInterface.NotImplementedMethod();
		}

		[Test]
		public void TestStrictness() {
			SimpleClass testClass = new SimpleClass();
			ISimpleInterface testClassProxy = (ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true);
			testClassProxy.Should().Not.Be.Null();

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test casting
			IImplemented implementedInterface = (IImplemented)testClassProxy;
			implementedInterface.ImplementedMethod();
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void TestStrictnessWithInvalidCast() {
			SimpleClass testClass = new SimpleClass();
			ISimpleInterface testClassProxy = (ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true);
			testClassProxy.Should().Not.Be.Null();

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test invalid cast
			INotImplemented notImplementedInterface = null;
			notImplementedInterface = (INotImplemented)testClassProxy;
		}


		[Test]
		public void TestStrictnessWithSupportedList() {
			SimpleClass testClass = new SimpleClass();
			ISimpleInterface testClassProxy = (ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true, new Type[] { typeof(INotImplemented) });
			testClassProxy.Should().Not.Be.Null();

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test casting
			IImplemented implementedInterface = (IImplemented)testClassProxy;
			implementedInterface.Should().Not.Be.Null();
			implementedInterface.ImplementedMethod();

			// Test invalid cast but which is supported via the supported type list
			INotImplemented notImplementedInterface = null;
			notImplementedInterface = (INotImplemented)testClassProxy;
			notImplementedInterface.Should().Not.Be.Null();
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void TestStrictnessWithSupportedListAndInvalidCast() {
			SimpleClass testClass = new SimpleClass();
			ISimpleInterface testClassProxy = (ISimpleInterface) DynamicProxyFactory.Instance.CreateProxy(testClass, new InvocationDelegate(InvocationHandler), true, new Type[] { typeof(INotImplemented) });
			testClassProxy.Should().Not.Be.Null();

			// No test for method 1, just make sure it doesn't bomb ;-)
			testClassProxy.Method1();

			// Test invalid cast
			INotImplemented2 notImplementedInterface2 = null;
			notImplementedInterface2 = (INotImplemented2)testClassProxy;
		}

    	private object InvocationHandler(object target, MethodBase method, object[] parameters) {
			//Console.WriteLine("Before: " + method.Name);
			object result = method.Invoke(target, parameters);
			//Console.WriteLine("After: " + method.Name);
			return result;
		}

    }

    #region Samples
    public interface IImplemented
    {
        void ImplementedMethod();
    }

    public interface INotImplemented2
    {
    }
    public interface ISimpleInterface
    {
        void Method1();
        string Method2();
        int Method3();
        int Method4(int inValue);
        void Method5(int inValue, out int outValue);
        void Method6(ref int value);
    }
    public interface INotImplemented
    {
        void NotImplementedMethod();
    }

    public class SimpleClass : ISimpleInterface, IImplemented
    {
        public SimpleClass()
        {
        }

        public void Method1()
        {
            //Console.WriteLine("    Method 1 called");
        }

        public string Method2()
        {
            //Console.WriteLine("    Method 2 called, returning 'Hello World!'");
            return "Hello World!";
        }

        public int Method3()
        {
            //Console.WriteLine("    Method 3 called, returning 10000");
            return 10000;
        }

        public int Method4(int inValue)
        {
            //Console.WriteLine("    Method 4 called, returning inValue: " + inValue);
            return inValue;
        }

        public void Method5(int inValue, out int outValue)
        {
            //Console.WriteLine("    Method 5 called. Setting outValue to the value of the inValue: " + inValue);
            outValue = inValue;
        }

        public void Method6(ref int value)
        {
            //Console.WriteLine("    Method 5 called. Changing value from: " + value + " to: 98765");
            value = 98765;
        }

        public void ImplementedMethod()
        {
            //Console.WriteLine("Implemented method called");
        }

    }

    #endregion
}
