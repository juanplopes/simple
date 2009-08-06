using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.ConfigSource;
using Simple.Services;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class ServiceConfigFixture
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

        [Test]
        public void TestRefCall()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();

            int a = 42;
            svc.TestRefMethod(ref a);
            Assert.AreEqual(42, a);
        }


        [Test]
        public void TestOutCall()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();

            int a;
            svc.TestOutMethod(out a);
            Assert.AreEqual(0, a);
        }
    }

    public interface ITestClientConnector
    {
        int TestInt();
        string TestString();
        void TestVoid();
        void TestRefMethod(ref int a);
        void TestOutMethod(out int a);

    }
}
