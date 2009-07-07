using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Services;
using Simple.ConfigSource;
using Simple.Remoting;
using Simple.Server;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Reflection;

namespace Simple.Tests.Service
{
    [TestClass]
    public class RemotingFactoryFixture
    {
        static Process process;

        [ClassInitialize]
        public static void ClassSetup(TestContext context)
        {
            process = Process.Start(new ProcessStartInfo()
            {
                FileName = Assembly.GetExecutingAssembly().Location,
                Arguments = Server.RemotingTest,
                WindowStyle = ProcessWindowStyle.Hidden,
            });

        }

        [ClassCleanup]
        public static void ClassTeardown()
        {
            process.Kill();
        }


        protected Guid GetSource()
        {
            Guid guid = Guid.NewGuid();

            RemotingSimply.Do.Configure(guid,
                XmlConfig.LoadXml<RemotingConfig>(RemotingConfigs.SimpleRemotingConfig));

            return guid;
        }

        protected void ReleaseSource(Guid guid)
        {
            SourceManager.RemoveSource<RemotingConfig>(guid);
            SourceManager.RemoveSource<IServiceHostProvider>(guid);
            SourceManager.RemoveSource<IServiceClientProvider>(guid);
        }

        [TestMethod]
        public void SimpleServiceMarshalingTest()
        {
            Guid guid = GetSource();

            ISimpleService service = Simply.Get(guid).Connect<ISimpleService>();

            Assert.AreEqual(42, service.GetInt32());
            Assert.AreEqual("whatever", service.GetString());

            ReleaseSource(guid);
            
            service = Simply.Get(guid).Connect<ISimpleService>();
            Assert.AreEqual(0, service.GetInt32());
            Assert.AreEqual(null, service.GetString());
        }

        [TestMethod, ExpectedException(typeof(RemotingException))]
        public void TestFailConnect()
        {
            Guid guid = GetSource();

            IFailService service = Simply.Get(guid).Connect<IFailService>();

            Assert.AreEqual(84, service.FailInt());
        }

        [TestMethod]
        public void SimpleBigMarshalingTest()
        {
            Guid guid = GetSource();

            ISimpleService service = Simply.Get(guid).Connect<ISimpleService>();

            Assert.AreEqual(500000, service.GetByteArray(500000).Length);

            ReleaseSource(guid);

            service = Simply.Get(guid).Connect<ISimpleService>();
            Assert.AreEqual(null, service.GetByteArray(100));

        }

        [TestMethod]
        public void ConnectToSecondServiceTest()
        {
            Guid guid = GetSource();

            ISecondService service = Simply.Get(guid).Connect<ISecondService>();

            Assert.AreEqual("42", service.OtherString());
        }

        [TestMethod]
        public void TestCreateSameServiceTwice()
        {
            for (int i = 0; i < 3; i++)
            {
                SimpleServiceMarshalingTest();
            }
           
        }
    }

    #region Samples
    [MainContract]
    public interface ISimpleService
    {
        string GetString();
        int GetInt32();
        byte[] GetByteArray(int size);
    }

    [MainContract]
    public interface IFailService
    {
        int FailInt();
    }

    [MainContract]
    public interface ISecondService
    {
        string OtherString();
    }

    public class FailConnectService : IFailService
    {
        public int FailInt()
        {
            return 84;
        }
    }

    public class SimpleService : MarshalByRefObject, ISimpleService, ISecondService
    {

        #region ISimpleService Members

        public string GetString()
        {
            return "whatever";
        }

        public int GetInt32()
        {
            return 42;
        }

        public byte[] GetByteArray(int size)
        {
            return new byte[size];
        }
        #endregion


        #region ISecondService Members

        public string OtherString()
        {
            return "42";
        }

        #endregion
    }

    #endregion
}
