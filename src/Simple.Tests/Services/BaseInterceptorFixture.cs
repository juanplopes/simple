using System;
using System.Security.Principal;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Services;

namespace Simple.Tests.Services
{
    public abstract partial class BaseInterceptorFixture
    {
        public static void ConfigureSvcs(Guid guid)
        {
            ConfigureSvcsWithoutHooks(guid);
            ConfigureServerHooks(guid);
        }

        public static void ConfigureSvcsWithoutHooks(Guid guid)
        {
            Simply.Do[guid].Host(typeof(TestService));
            Simply.Do[guid].Host(typeof(OtherService));
            Simply.Do[guid].Host(typeof(FindMeService));
        }

        public static void ConfigureClientHooks(Guid guid)
        {
            ConfigureGenericHooks(x => Simply.Do[guid].AddClientHook(x));
            ConfigureClientServerHooks(guid);
        }

        public static void ConfigureClientServerHooks(Guid guid)
        {
            Simply.Do[guid].AddClientHook(x => new WindowsIdentityInjector(x));
        }

        public static void ConfigureServerHooks(Guid guid)
        {
            ConfigureGenericHooks(x => Simply.Do[guid].AddServerHook(x));
        }

        public static void ConfigureGenericHooks(Action<Func<CallHookArgs, ICallHook>> action)
        {
            action(HookFunc<ITestService>(x => new TestCallHook(x)));
            action(HookFunc<IFindMeService>(x => new AttributeFinderCallHook(x)));
            action(HookFunc<IOtherService>(x => new TestCallHookString(x)));
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
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnString(4, "1", "2", "2", "4", "2").Should().Be("42");
        }

        [Test]
        public virtual void TestFindAttributeInConcreteClass()
        {
            IFindMeService test = Simply.Do[ConfigKey].Resolve<IFindMeService>();
            test.FindMe().Should().Be(true);
        }

        [Test]
        public void TestIdentityMatch()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            var ident = test.ReturnIdentityString();
            ident.Should().Be(WindowsIdentity.GetCurrent().Name);
        }

        [Test]
        public void TestIdentityMatchGenerics()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            var ident = test.ReturnIdentityStringG<double>(2);
            ident.Should().Be(WindowsIdentity.GetCurrent().Name+"2");
        }

        [Test]
        public void TestGenerics()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnDoubleG("42.5").Should().Be(42.5);
        }

        [Test]
        public void TestGenericsVoid()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnVoidG("42.5");
        }

        [Test]
        public void TestSelectiveHook()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnString().Should().Not.Be("123456");

            IOtherService other = Simply.Do[ConfigKey].Resolve<IOtherService>();
            other.ReturnString().Should().Be("123456");
        }

        [Test]
        public void TestNonHookedException()
        {
            IOtherService other = new OtherService();
            other.ThrowException().Should().Be(42.42f);
        }

        [Test]
        public void TestNonHookedExceptionOnFinally()
        {
            IOtherService other = new OtherService();
            other.ThrowExceptionOnFinally().Should().Be(new DateTime(2009, 09, 09));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestHookException()
        {
            IOtherService other = Simply.Do[ConfigKey].Resolve<IOtherService>();
            other.ThrowException();
        }


        [Test, ExpectedException(typeof(ArithmeticException))]
        public void TestHookExceptionOnFinally()
        {
            IOtherService other = Simply.Do[ConfigKey].Resolve<IOtherService>();
            other.ThrowExceptionOnFinally();
        }

        [Test]
        public void TestOutParams()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            string res;
            test.ReturnVoid(out res);
            res.Should().Be("42");
        }

        [Test]
        public void TestRefParams()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            string res = "10";
            double intRes = test.ReturnDouble(ref res);
            intRes.Should().Be(10.0);
            res.Should().Be("42");
        }

        [Test]
        public void TestVoid()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnVoid();
        }

        [Test]
        public void TestInt()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnInt().Should().Be(42);
        }

        [Test]
        public virtual void TestGenericInt()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnIntG("44").Should().Be(42);
        }

        [Test]
        public void TestString()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnString().Should().Be("10");
        }

        [Test]
        public void TestVoidInt()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnVoid(10);
        }

        [Test]
        public void TestIntInt()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnInt(30).Should().Be(42);
        }

        [Test]
        public void TestStringInt()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ReturnString(42).Should().Be("42");
        }

        [Test, ExpectedException(typeof(ApplicationException), ExpectedMessage = "AAA")]
        public void TestException()
        {
            ITestService test = Simply.Do[ConfigKey].Resolve<ITestService>();
            test.ThrowException();
        }

    }
}
