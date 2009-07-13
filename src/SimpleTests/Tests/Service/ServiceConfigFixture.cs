using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.ConfigSource;
using Simple.Services;
using Simple.Client;

namespace Simple.Tests.ConfigSource
{
    [TestClass]
    public class ServiceObjectsFixture
    {
        [TestMethod]
        public void TestNullServiceCreation()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();

            Assert.AreEqual(0, svc.TestInt());
            Assert.AreEqual(null, svc.TestString());
        }

        [TestMethod]
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
