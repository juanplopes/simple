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
    public abstract partial class BaseInterceptorFixture
    {
        public static void ConfigureSvcs(Guid guid)
        {
            Simply.Get(guid).Host(typeof(TestService));
            Simply.Get(guid).Host(typeof(OtherService));

            Simply.Get(guid).AddServerHook(x => new TestCallHook(x));
            Simply.Get(guid).AddServerHook(TestCallHookString.MyFunc);
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
        public void TestSelectiveHook()
        {
            ITestService test = Simply.Get(ConfigKey).Resolve<ITestService>();
            Assert.AreNotEqual("123456", test.TestString());

            IOtherService other = Simply.Get(ConfigKey).Resolve<IOtherService>();
            Assert.AreEqual("123456", other.SomeStringFunction());
        }

        [Test]
        public void TestNonHookedException()
        {
            IOtherService other = new OtherService();
            Assert.AreEqual(42.42f, other.ExceptionFunction());
        }


        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestHookException()
        {
            IOtherService other = Simply.Get(ConfigKey).Resolve<IOtherService>();
            other.ExceptionFunction();
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
