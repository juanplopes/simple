using System;
using System.Runtime.Remoting;
using NUnit.Framework;
using Simple.Config;
using Simple.Services;

namespace Simple.Tests.Services
{
    [TestFixture]
    public class ServiceConfigFixture
    {
        [Test, ExpectedException(typeof(NotImplementedException))]
        public void TestNullServiceCreation()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();

            Assert.AreEqual(0, svc.TestInt());
            Assert.AreEqual(null, svc.TestString());
        }

        [Test]
        public void TestReconfigure()
        {
            Guid guid = Guid.NewGuid();

            Simply.Do[guid].Configure.DefaultHost();
            Simply.Do[guid].Host(typeof(SimpleService));

            Assert.AreEqual(42, Simply.Do[guid].Resolve<ISimpleService>().GetInt32());

            Simply.Do[guid].Configure.Remoting().FromXmlString(Helper.MakeConfig(Helper.MakeUri("http", 8001)));

            Assert.AreEqual(42, Simply.Do[guid].Resolve<ISimpleService>().GetInt32());

            Simply.Do[guid].Release.Remoting();
        }

        [Test]
        public void TestUnconfigure()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            Simply.Do[guid1].Configure.Remoting().FromXmlString(Helper.MakeConfig(Helper.MakeUri("http", 8002)));
            Simply.Do[guid2].Configure.Remoting().FromXmlString(Helper.MakeConfig(Helper.MakeUri("http", 8002)));

            Simply.Do[guid1].Host(typeof(SimpleService));
            Assert.AreEqual(42, Simply.Do[guid2].Resolve<ISimpleService>().GetInt32());

            Simply.Do[guid1].Configure.DefaultHost();

            Assert.Throws(typeof(RemotingException), () =>
            {
                Assert.AreEqual(42, Simply.Do[guid2].Resolve<ISimpleService>().GetInt32());
            });
        }

        [Test, ExpectedException(typeof(NotImplementedException))]
        public void TestVoidCall()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();
            svc.TestVoid();
        }

        [Test, ExpectedException(typeof(NotImplementedException))]
        public void TestRefCall()
        {
            SourceManager.Do.Remove<IServiceClientProvider>(this);
            var svc = Simply.Do.Resolve<ITestClientConnector>();

            int a = 42;
            svc.TestRefMethod(ref a);
            Assert.AreEqual(42, a);
        }


        [Test, ExpectedException(typeof(NotImplementedException))]
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
