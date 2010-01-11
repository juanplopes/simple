using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services.Remoting;
using Simple.Config;
using Simple.Services;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Simple.Tests.Services
{
    public abstract class BaseSelfRemotingFactoryFixture : BaseFactoryFixture
    {
        public abstract Uri Uri { get; }

        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();

            Simply.Do[guid].Configure.Remoting().FromXmlString(Helper.MakeConfig(Uri));
            Simply.Do[guid].Host(typeof(SimpleService));

            return guid;
        }

        protected override void Release(Guid guid)
        {
            Simply.Do[guid].StopServer();
            Simply.Do[guid].Release.Remoting();
        }

        [Test]
        public void TestAllCallsAreInSameThread()
        {
            var service = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            Assert.IsTrue(service.CheckSameThread(Thread.CurrentThread.ManagedThreadId));
        }
    }
}
