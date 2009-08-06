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
using System.Security.Principal;

namespace Simple.Tests.Service
{
    public abstract class BaseFactoryFixture
    {
        protected abstract Guid Configure();
        protected abstract void Release(Guid guid);
        protected Guid ConfigKey { get; set; }

        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigKey = Configure();
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            Release(ConfigKey);
        }


        [Test]
        public void SimpleServiceMarshalingTest()
        {
            ISimpleService service = Simply.Get(ConfigKey).Resolve<ISimpleService>();

            Assert.AreEqual(42, service.GetInt32());
            Assert.AreEqual("whatever", service.GetString());
        }

        [Test]
        public void HeaderPassingTest()
        {
            ISimpleService service = Simply.Get(ConfigKey).Resolve<ISimpleService>();

            CallHeaders.Do["returnMe"] = "123";
            Assert.AreEqual("123", service.TestHeaderPassing());

            CallHeaders.Do["returnMe"] = "1234";
            Assert.AreEqual("1234", service.TestHeaderPassing());
        }

        [Test]
        public void WrongPassedIdentityTest()
        {
            ISimpleService service = Simply.Get(ConfigKey).Resolve<ISimpleService>();
            SimpleContext.Get().Username = null;
            Assert.IsFalse(service.TestPassedIdentity());
        }

        [Test]
        public void HeaderPassingAndReturningTest()
        {
            ISimpleService service = Simply.Get(ConfigKey).Resolve<ISimpleService>();

            CallHeaders.Do["returnMe"] = 12345;
            Assert.AreEqual(12345, service.TestHeaderPassingAndReturning());
            Assert.AreEqual(12347, CallHeaders.Do["returnMe"]);

            CallHeaders.Do["returnMe"] = 666;
            Assert.AreEqual(666, service.TestHeaderPassingAndReturning());
            Assert.AreEqual(668, CallHeaders.Do["returnMe"]);

        }



        [Test, ExpectedException]
        public void TestFailConnect()
        {
            IFailService service = Simply.Get(ConfigKey).Resolve<IFailService>();
            Assert.AreEqual(84, service.FailInt());
        }

        [Test]
        public void TestPostFailConnectState()
        {
            bool ex = false;
            try
            {
                IFailService service = Simply.Get(ConfigKey).Resolve<IFailService>();
                service.FailInt();
            }
            catch (Exception)
            {
                ex = true;
            }

            Assert.IsTrue(ex);
            ISecondService service2 = Simply.Get(ConfigKey).Resolve<ISecondService>();
            Assert.AreEqual("42", service2.OtherString());
        }

        [Test]
        public void SimpleBigMarshalingTest()
        {
            ISimpleService service = Simply.Get(ConfigKey).Resolve<ISimpleService>();

            Assert.AreEqual(500000, service.GetByteArray(500000).Length);
        }

        [Test]
        public void ConnectToSecondServiceTest()
        {
            ISecondService service = Simply.Get(ConfigKey).Resolve<ISecondService>();
            Assert.AreEqual("42", service.OtherString());
        }

        [Test]
        public void TestManyCalls()
        {
            for (int i = 0; i < 50; i++)
            {
                Simply.Do.Log(this).DebugFormat("Running {0}...", i);
                ISecondService service = Simply.Get(ConfigKey).Resolve<ISecondService>();
                Assert.AreEqual("42", service.OtherString());
            }
        }

        [Test]
        public void MarshalOtherServiceTest()
        {
            ISecondService service = Simply.Get(ConfigKey).Resolve<ISecondService>();
            IFailService serviceFail = service.GetOtherService(123);
            Assert.AreEqual(84, serviceFail.FailInt());
        }

        [Test]
        public void SerializeComplexType()
        {
            ISecondService service = Simply.Get(ConfigKey).Resolve<ISecondService>();
            Assert.AreEqual("whatever", service.GetComplexType().Oi);
            Assert.AreEqual(42, service.GetComplexType().Tchau);

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
        string TestHeaderPassing();
        int TestHeaderPassingAndReturning();
        bool TestPassedIdentity();
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

        public string TestHeaderPassing()
        {
            return (string)CallHeaders.Do["returnMe"];
        }

        public int TestHeaderPassingAndReturning()
        {
            int a = (int)CallHeaders.Do["returnMe"];
            CallHeaders.Do["returnMe"] =  a + 2;
            return a;
        }

        public bool TestPassedIdentity()
        {
            var userName = SimpleContext.Get().Username;
            return WindowsIdentity.GetCurrent().Name == userName;
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
