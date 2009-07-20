using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.ConfigSource;
using Simple.Services;
using Simple.Client;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class ServiceObjectsFixture
    {
        [Test]
        public void TestNullServiceCreation()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();

            Assert.AreEqual(0, svc.TestInt());
            Assert.AreEqual(null, svc.TestString());
        }

        [Test]
        public void TestVoidCall()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();
            svc.TestVoid();
        }
    }

    public interface ITestClientConnector
    {
        int TestInt();
        string TestString();
        void TestVoid();
    }
}
