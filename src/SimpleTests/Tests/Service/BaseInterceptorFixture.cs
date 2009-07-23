using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;
using Simple.Reflection;
using NUnit.Framework;
using Simple.Server;
using Simple.ConfigSource;

namespace Simple.Tests.Service
{
    public abstract class BaseInterceptorFixture
    {
        class Interceptor : IInterceptor
        {
            #region IInterceptor Members

            public object Intercept(object obj, System.Reflection.MethodBase info, object[] parameters)
            {
                FastInvoker invoker = new FastInvoker(info);
                if (invoker.Method.ReturnType == typeof(int))
                {
                    return 42;
                }
                else
                {
                    return invoker.Invoke(obj, parameters);
                }
            }

            #endregion
        }
        public interface ITestService : IService
        {
            void TestVoid();
            int TestInt();
            string TestString();
            void TestVoidInt(int i);
            int TestIntInt(int i);
            string TestStringInt(int i);
            void TestException();
        }
        public class TestService : MarshalByRefObject, ITestService
        {
            public void TestVoid() { }
            public int TestInt() { return 10; }
            public string TestString() { return "10"; }
            public void TestVoidInt(int i) { }
            public int TestIntInt(int i) { return i; }
            public string TestStringInt(int i) { return i.ToString(); }
            public void TestException() { throw new ApplicationException("AAA"); }
        }


        protected abstract Guid Configure();
        protected abstract void Release(Guid guid);
        protected Guid ConfigKey { get; set; }
        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigKey = Configure();
            Simply.Get(ConfigKey).Host(typeof(TestService), new Interceptor());
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            Release(ConfigKey);
        }


        [Test]
        public void TestVoid()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            test.TestVoid();
        }

        [Test]
        public void TestInt()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreEqual(42, test.TestInt());
        }

        [Test]
        public void TestString()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreEqual("10", test.TestString());
        }

        [Test]
        public void TestVoidInt()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            test.TestVoidInt(10);
        }

        [Test]
        public void TestIntInt()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreEqual(42, test.TestIntInt(30));
        }

        [Test]
        public void TestStringInt()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreEqual("42", test.TestStringInt(42));
        }

        [Test, ExpectedException(typeof(ApplicationException), ExpectedMessage = "AAA")]
        public void TestException()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            test.TestException();
        }

    }
}
