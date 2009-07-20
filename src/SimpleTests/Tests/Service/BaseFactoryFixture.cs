using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Services;
using Simple.ConfigSource;
using Simple.Services.Remoting;
using Simple.Server;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Simple.Tests.Service
{
    public abstract class BaseFactoryFixture
    {
        protected abstract Guid GetSource();
        protected abstract void ReleaseSource(Guid guid);

        [Test]
        public void SimpleServiceMarshalingTest()
        {
            Guid guid = GetSource();

            ISimpleService service = Simply.Get(guid).Resolve<ISimpleService>();

            Assert.AreEqual(42, service.GetInt32());
            Assert.AreEqual("whatever", service.GetString());

            ReleaseSource(guid);
            
            service = Simply.Get(guid).Resolve<ISimpleService>();
            Assert.AreEqual(0, service.GetInt32());
            Assert.AreEqual(null, service.GetString());
        }

        [Test, ExpectedException(typeof(ServiceConnectionException))]
        public void TestFailConnect()
        {
            Guid guid = GetSource();

            IFailService service = Simply.Get(guid).Resolve<IFailService>();

            Assert.AreEqual(84, service.FailInt());
        }

        [Test]
        public void SimpleBigMarshalingTest()
        {
            Guid guid = GetSource();

            ISimpleService service = Simply.Get(guid).Resolve<ISimpleService>();

            Assert.AreEqual(500000, service.GetByteArray(500000).Length);

            ReleaseSource(guid);

            service = Simply.Get(guid).Resolve<ISimpleService>();
            Assert.AreEqual(null, service.GetByteArray(100));

        }

        [Test]
        public void ConnectToSecondServiceTest()
        {
            Guid guid = GetSource();

            ISecondService service = Simply.Get(guid).Resolve<ISecondService>();

            Assert.AreEqual("42", service.OtherString());
        }

        [Test]
        public void TestCreateSameServiceTwice()
        {
            for (int i = 0; i < 3; i++)
            {
                SimpleServiceMarshalingTest();
            }
           
        }
    }

    #region Samples
    public interface ISimpleService : IService
    {
        string GetString();
        int GetInt32();
        byte[] GetByteArray(int size);
    }

    public interface IFailService : IService
    {
        int FailInt();
    }

    public interface ISecondService : IService
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
