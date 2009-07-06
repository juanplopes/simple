using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Services;
using Simple.ConfigSource;
using Simple.Remoting;
using Simple.Server;

namespace Simple.Tests.Service
{
    [TestClass]
    public class RemotingFactoryFixture
    {
        protected Guid GetSource()
        {
            Guid guid = Guid.NewGuid();

            SourceManager.RegisterSource(guid, new XmlConfigSource<RemotingConfig>().Load(RemotingConfigs.SimpleRemotingConfig));
            
            var hostProvider = new RemotingHostProvider();
            SourceManager.Configure(guid, hostProvider);

            var clientProvider = new RemotingClientProvider();
            SourceManager.Configure(guid, clientProvider);

            SourceManager.RegisterSource(guid, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            SourceManager.RegisterSource(guid, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

            return guid;
        }

        [TestMethod]
        public void SimpleServiceMarshalingTest()
        {
            Guid guid = GetSource();

            Simply.Get(guid).Host(typeof(SimpleService));
            ISimpleService service = Simply.Get(guid).Connect<ISimpleService>();

            Assert.AreEqual(42, service.GetInt32());
            Assert.AreEqual("whatever", service.GetString());
        }

        [TestMethod]
        public void SimpleBigMarshalingTest()
        {
            Guid guid = GetSource();

            Simply.Get(guid).Host(typeof(SimpleService));
            ISimpleService service = Simply.Get(guid).Connect<ISimpleService>();

            Assert.AreEqual(500000, service.GetByteArray(500000).Length);
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

    public class SimpleService : MarshalByRefObject, ISimpleService
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
    }

    #endregion
}
