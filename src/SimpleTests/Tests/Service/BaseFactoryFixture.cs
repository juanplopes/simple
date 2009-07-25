using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Services;
using Simple.ConfigSource;
using Simple.Services.Remoting;
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
            try
            {
                IFailService service = Simply.Get(guid).Resolve<IFailService>();
                Assert.AreEqual(84, service.FailInt());
            }
            finally
            {
                ReleaseSource(guid);
            }
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
            ReleaseSource(guid);
        }

        [Test]
        public void TestManyCalls()
        {
            Guid guid = GetSource();

            for (int i = 0; i < 50; i++)
            {
                ISecondService service = Simply.Get(guid).Resolve<ISecondService>();
                Assert.AreEqual("42", service.OtherString());
            }
            ReleaseSource(guid);

        }

        [Test]
        public void MarshalOtherServiceTest()
        {
            Guid guid = GetSource();

            ISecondService service = Simply.Get(guid).Resolve<ISecondService>();
            IFailService serviceFail = service.GetOtherService(123);
            Assert.AreEqual(84, serviceFail.FailInt());
            ReleaseSource(guid);

        }

        [Test]
        public void SerializeComplexType()
        {
            Guid guid = GetSource();

            ISecondService service = Simply.Get(guid).Resolve<ISecondService>();
            Assert.AreEqual("whatever", service.GetComplexType().Oi);
            Assert.AreEqual(42, service.GetComplexType().Tchau);
            ReleaseSource(guid);

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
        IFailService GetOtherService(int number);
        ComplexType GetComplexType();
    }

    public class FailConnectService : MarshalByRefObject, IFailService
    {
        public int FailInt()
        {
            return 84;
        }
    }

    [Serializable]
    public class ComplexType
    {
        public string Oi { get; set; }
        public int Tchau { get; set; }
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

        #region ISecondService Members


        public IFailService GetOtherService(int number)
        {
            return new FailConnectService();
        }

        #endregion

        #region ISecondService Members


        public ComplexType GetComplexType()
        {
            return new ComplexType()
            {
                Oi = "whatever",
                Tchau = 42
            };
        }

        #endregion
    }

    #endregion
}
