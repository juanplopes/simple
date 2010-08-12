using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Services;

namespace Simple.Tests.Services
{
    [TestFixture]
    public class HookOrderServerFixture : BaseHookOrderFixture
    {
        public override Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc1
        {
            get { return (g, h) => { Simply.Do[g].AddServerHook(h); }; }
        }

        public override Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc2
        {
            get { return HookFunc1; }
        }
    }

    [TestFixture]
    public class HookOrderClientFixture : BaseHookOrderFixture
    {
        public override Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc1
        {
            get { return (g, h) => { Simply.Do[g].AddClientHook(h); }; }
        }
        public override Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc2
        {
            get { return HookFunc1; }
        }
    }
    [TestFixture]
    public class HookOrderMixedFixture : BaseHookOrderFixture
    {
        public override Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc1
        {
            get { return (g, h) => { Simply.Do[g].AddServerHook(h); }; }
        }
        public override Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc2
        {
            get { return (g, h) => { Simply.Do[g].AddClientHook(h); }; }
        }
    }

    public abstract class BaseHookOrderFixture
    {
        public abstract Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc1 { get; }
        public abstract Action<Guid, Func<CallHookArgs, ICallHook>> HookFunc2 { get; }


        public class AddOneHook : BaseCallHook
        {
            public AddOneHook(CallHookArgs args) : base(args) { }

            public override void Before()
            {
                if (CallArgs.Args.Length > 0) CallArgs.Args[0] = ((int)CallArgs.Args[0]) + 1;
            }

            public override void AfterSuccess()
            {
                this.CallArgs.Return = ((int)this.CallArgs.Return) + 1;
            }
        }

        public class Set9998AfterHook : BaseCallHook
        {
            public Set9998AfterHook(CallHookArgs args) : base(args) { }

            public override void AfterSuccess()
            {
                this.CallArgs.Return = 9998;
            }
        }

        public class Set9998BeforeHook : BaseCallHook
        {
            public Set9998BeforeHook(CallHookArgs args) : base(args) { }

            public override void Before()
            {
                if (this.CallArgs.Args.Length > 0)
                    this.CallArgs.Args[0] = 9998;
            }
        }


        protected Guid Configure()
        {
            Guid guid = Guid.NewGuid();
            Simply.Do[guid].Configure.DefaultHost();

            Simply.Do[guid].Host(typeof(SimpleService));
            Simply.Do[guid].Host(typeof(BaseInterceptorFixture.TestService));

            return guid;
        }

        protected void Release(Guid guid)
        {
            Simply.Do[guid].StopServer();
        }

        [Test]
        public void TestHookOrderPremises()
        {
            Guid guid = Configure();

            var svc1 = Simply.Do[guid].Resolve<ISimpleService>();
            svc1.GetInt32().Should().Be(42);

            var svc2 = Simply.Do[guid].Resolve<BaseInterceptorFixture.ITestService>();
            svc2.ReturnInt(1000).Should().Be(1000);
            svc2.ReturnInt(1001).Should().Be(1001);

            Release(guid);
        }

        [Test, ExpectedException(typeof(ServiceConnectionException))]
        public void TestServiceStopping()
        {
            Guid guid = Configure();
            Release(guid);
            var svc1 = Simply.Do[guid].Resolve<ISimpleService>();
        }

        [Test]
        public void TestHookOrderSetExternalAddInternal()
        {
            Guid guid = Configure();
            HookFunc1(guid, x => new AddOneHook(x));
            HookFunc2(guid, x => new Set9998AfterHook(x));

            var svc = Simply.Do[guid].Resolve<ISimpleService>();
            svc.GetInt32().Should().Be(9998);

            Release(guid);
        }

        [Test]
        public void TestHookOrderSetInternalAddExternal()
        {
            Guid guid = Configure();
            HookFunc1(guid, x => new Set9998AfterHook(x));
            HookFunc2(guid, x => new AddOneHook(x));

            var svc = Simply.Do[guid].Resolve<ISimpleService>();
            svc.GetInt32().Should().Be(9999);

            Release(guid);
        }

        [Test]
        public void TestHookOrderBeforeSetExternalAddInternal()
        {
            Guid guid = Configure();
            HookFunc1(guid, x => new AddOneHook(x));
            HookFunc2(guid, x => new Set9998BeforeHook(x));

            var svc = Simply.Do[guid].Resolve<BaseInterceptorFixture.ITestService>();
            svc.ReturnInt(666).Should().Be(10000);

            Release(guid);
        }

        [Test]
        public void TestHookOrderBeforeSetInternalAddExternal()
        {
            Guid guid = Configure();
            HookFunc1(guid, x => new Set9998BeforeHook(x));
            HookFunc2(guid, x => new AddOneHook(x));

            var svc = Simply.Do[guid].Resolve<BaseInterceptorFixture.ITestService>();
            svc.ReturnInt(777).Should().Be(9999);

            Release(guid);
        }
    }
}
