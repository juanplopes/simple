using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Services;
using Simple.Services.Default;

namespace Simple.Tests.Service
{
    [TestFixture]
    class MiscServicesFixture
    {
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
            DefaultHostSimply.Do.Configure(guid);

            Simply.Get(guid).Host(typeof(SimpleService));
            Simply.Get(guid).Host(typeof(BaseInterceptorFixture.TestService));

            return guid;
        }

        protected void Release(Guid guid)
        {
            Simply.Get(guid).StopServer();
        }

        [Test]
        public void TestHookOrderPremises()
        {
            Guid guid = Configure();

            var svc1 = Simply.Get(guid).Resolve<ISimpleService>();
            Assert.AreEqual(42, svc1.GetInt32());

            var svc2 = Simply.Get(guid).Resolve<BaseInterceptorFixture.ITestService>();
            Assert.AreEqual(1000, svc2.TestIntInt(1000));
            Assert.AreEqual(1001, svc2.TestIntInt(1001));

            Release(guid);
        }

        [Test, ExpectedException(typeof(ServiceConnectionException))]
        public void TestServiceStopping()
        {
            Guid guid = Configure();
            Release(guid);
            var svc1 = Simply.Get(guid).Resolve<ISimpleService>();
        }

        [Test]
        public void TestHookOrderSetExternalAddInternal()
        {
            Guid guid = Configure();
            Simply.Get(guid).AddServerHook(x => new AddOneHook(x));
            Simply.Get(guid).AddServerHook(x => new Set9998AfterHook(x));

            var svc = Simply.Get(guid).Resolve<ISimpleService>();
            Assert.AreEqual(9998, svc.GetInt32());

            Release(guid);
        }

        [Test]
        public void TestHookOrderSetInternalAddExternal()
        {
            Guid guid = Configure();
            Simply.Get(guid).AddServerHook(x => new Set9998AfterHook(x));
            Simply.Get(guid).AddServerHook(x => new AddOneHook(x));

            var svc = Simply.Get(guid).Resolve<ISimpleService>();
            Assert.AreEqual(9999, svc.GetInt32());

            Release(guid);
        }

        [Test]
        public void TestHookOrderBeforeSetExternalAddInternal()
        {
            Guid guid = Configure();
            Simply.Get(guid).AddServerHook(x => new AddOneHook(x));
            Simply.Get(guid).AddServerHook(x => new Set9998BeforeHook(x));

            var svc = Simply.Get(guid).Resolve<BaseInterceptorFixture.ITestService>();
            Assert.AreEqual(10000, svc.TestIntInt(666));

            Release(guid);
        }

        [Test]
        public void TestHookOrderBeforeSetInternalAddExternal()
        {
            Guid guid = Configure();
            Simply.Get(guid).AddServerHook(x => new Set9998BeforeHook(x));
            Simply.Get(guid).AddServerHook(x => new AddOneHook(x));

            var svc = Simply.Get(guid).Resolve<BaseInterceptorFixture.ITestService>();
            Assert.AreEqual(9999, svc.TestIntInt(777));

            Release(guid);
        }
    }
}
