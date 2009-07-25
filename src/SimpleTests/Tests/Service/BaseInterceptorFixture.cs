using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;
using Simple.Reflection;
using NUnit.Framework;
using Simple.ConfigSource;
using System.Reflection;
using System.Globalization;

namespace Simple.Tests.Service
{
    public abstract class BaseInterceptorFixture
    {
        public class Interceptor : BaseInterceptor
        {
            #region IInterceptor Members

            public override object Intercept(object target, MethodBase method, object[] args)
            {
                if ((method as MethodInfo).ReturnType==typeof(int))
                {
                    return 42;
                }
                else
                {
                    return Invoke(target, method, args);
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

            void TestGenericsVoid<T>(T value) where T : IConvertible;
            double TestGenerics<T>(T value) where T : IConvertible;
            double TestRefParams(ref string test);
            void TestOutParams(out string test);
            string TestParams(int prim, params string[] ult);
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

            public void TestGenericsVoid<T>(T value) where T : IConvertible
            {
                value.ToDouble(CultureInfo.InvariantCulture);
            }
            public double TestGenerics<T>(T value) where T : IConvertible
            {
                return value.ToDouble(CultureInfo.InvariantCulture);
            }

            public double TestRefParams(ref string test) { int res = int.Parse(test); test = "42"; return res; }
            public void TestOutParams(out string test) { test = "42"; }

            public string TestParams(int prim, params string[] ult)
            {
                return prim.ToString() + ult[ult.Length - 1];
            }
        }


        protected abstract Guid Configure();
        protected abstract void Release(Guid guid);
        protected Guid ConfigKey { get; set; }
        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigKey = Configure();
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            Release(ConfigKey);
        }

        [Test]
        public void TestParams()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreEqual("42", test.TestParams(4, "1", "2", "2", "4", "2"));
        }

        [Test]
        public void TestGenerics()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreEqual(42.5, test.TestGenerics("42.5"));
        }

        [Test]
        public void TestGenericsVoid()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            test.TestGenericsVoid("42.5");
        }

        [Test]
        public void TestOutParams()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            string res;
            test.TestOutParams(out res);
            Assert.AreEqual("42", res);
        }

        [Test]
        public void TestRefParams()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            string res = "10";
            double intRes = test.TestRefParams(ref res);
            Assert.AreEqual(10.0, intRes);
            Assert.AreEqual("42", res);
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
