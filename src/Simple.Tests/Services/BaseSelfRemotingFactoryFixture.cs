using System;
using System.Threading;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Services
{
    public abstract class BaseSelfRemotingFactoryFixture : BaseFactoryFixture
    {
        public abstract Uri Uri { get; }

        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();
            using (Simply.KeyContext(guid))
            {
                Simply.Do.Configure.Remoting().FromXmlString(Helper.MakeConfig(Uri));
                Simply.Do.Host(typeof(SimpleService));
            }

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
