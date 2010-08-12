using System;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.Expressions.Editable;
using Simple.Services;

namespace Simple.Tests.Services
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
        public void SimpleExpressionSerializationTest()
        {
            using (Simply.KeyContext(ConfigKey))
            {
                ISimpleService service = Simply.Do.Resolve<ISimpleService>();
                Expression<Predicate<int>> pred = i => i == 42;
                EditableExpression expr = EditableExpression.Create(Funcletizer.PartialEval(pred));

                service.TestExpression(expr, 41).Should().Be.False();
                service.TestExpression(expr, 42).Should().Be.True();
            }
        }

        [Test]
        public void StackReferenceExpressionSerializationTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            int hh = 42;
            Expression<Predicate<int>> pred = i => i == hh;
            EditableExpression expr = EditableExpression.Create(Funcletizer.PartialEval(pred));

            service.TestExpression(expr, 41).Should().Be.False();
            service.TestExpression(expr, 42).Should().Be.True();
        }

        [Test]
        public void SimpleServiceMarshalingTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();

            service.GetInt32().Should().Be(42);
            service.GetString().Should().Be("whatever");
        }

        [Test]
        public void SimpleMethodOverloadTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            service.GetOverloadedMethod(10).Should().Be(10);
            service.GetOverloadedMethod(10, 5).Should().Be(15);
        }

        [Test]
        public void SimplePropertyTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            service.SimpleProp.Should().Be(555);
        }

        [Test]
        public void SimpleIndexedPropertyTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            service[12345].Should().Be("12345");
        }


        [Test]
        public void HeaderPassingTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();

            CallHeaders.Do["returnMe"] = "123";
            service.TestHeaderPassing().Should().Be("123");

            CallHeaders.Do["returnMe"] = "1234";
            service.TestHeaderPassing().Should().Be("1234");
        }

        [Test]
        public void WrongPassedIdentityTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            SimpleContext.Get().Username = null;
            service.TestPassedIdentity().Should().Be.False();
        }

        [Test]
        public void HeaderPassingAndReturningTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();

            CallHeaders.Do["returnMe"] = 12345;
            service.TestHeaderPassingAndReturning().Should().Be(12345);
            CallHeaders.Do["returnMe"].Should().Be(12347);

            CallHeaders.Do["returnMe"] = 666;
            service.TestHeaderPassingAndReturning().Should().Be(666);
            CallHeaders.Do["returnMe"].Should().Be(668);

        }



        [Test, ExpectedException]
        public void TestFailConnect()
        {
            IFailService service = Simply.Do[ConfigKey].Resolve<IFailService>();
            service.FailInt().Should().Be(84);
        }

        [Test]
        public void TestFailConnectNotFailingWithTemporaryContext()
        {
            using (Simply.Do[ConfigKey].EnterServiceMockContext(typeof(IFailService), new FailConnectService()))
            {
                IFailService service = Simply.Do[ConfigKey].Resolve<IFailService>();
                service.FailInt().Should().Be(84);
            }

            Assert.That(() =>
            {
                IFailService service = Simply.Do[ConfigKey].Resolve<IFailService>();
                service.FailInt().Should().Be(84);
            }, Throws.Exception);
        }

        [Test]
        public void TestPostFailConnectState()
        {
            bool ex = false;
            try
            {
                IFailService service = Simply.Do[ConfigKey].Resolve<IFailService>();
                service.FailInt();
            }
            catch (Exception)
            {
                ex = true;
            }

            ex.Should().Be.True();
            ISecondService service2 = Simply.Do[ConfigKey].Resolve<ISecondService>();
            service2.OtherString().Should().Be("42");
        }

        [Test]
        public void SimpleBigMarshalingTest()
        {
            ISimpleService service = Simply.Do[ConfigKey].Resolve<ISimpleService>();

            service.GetByteArray(500000).Length.Should().Be(500000);
        }

        [Test]
        public void ConnectToSecondServiceTest()
        {
            ISecondService service = Simply.Do[ConfigKey].Resolve<ISecondService>();
            service.OtherString().Should().Be("42");
        }

        [Test]
        public void TestManyCalls()
        {
            for (int i = 0; i < 50; i++)
            {
                Simply.Do.Log(this).DebugFormat("Running {0}...", i);
                ISecondService service = Simply.Do[ConfigKey].Resolve<ISecondService>();
                service.OtherString().Should().Be("42");
            }
        }

        [Test]
        public void MarshalOtherServiceTest()
        {
            ISecondService service = Simply.Do[ConfigKey].Resolve<ISecondService>();
            IFailService serviceFail = service.GetOtherService(123);
            serviceFail.FailInt().Should().Be(84);
        }

        [Test]
        public void SerializeComplexType()
        {
            ISecondService service = Simply.Do[ConfigKey].Resolve<ISecondService>();
            service.GetComplexType().Oi.Should().Be("whatever");
            service.GetComplexType().Tchau.Should().Be(42);

        }

        [Test]
        public void TestSelfType()
        {
            SelfType type = new SelfType();
            type.SelfProperty = type;

            ISimpleService svc = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            svc.TestSelfType(type).Should().Be.True();
        }

        [Test]
        public void TestCreateSameServiceTwice()
        {
            for (int i = 0; i < 3; i++)
            {
                SimpleServiceMarshalingTest();
            }

        }

        [Test, Explicit("cannot serialize lambda expressions in remoting right now")]
        public void TestSerializingLambdaExpressions()
        {
            ISimpleService svc = Simply.Do[ConfigKey].Resolve<ISimpleService>();
            svc.Calculate((x, y) => x * 2 * y, 3, 7).Should().Be(42);
        }

        //[Test]
        //public void TestSerializingLambdaExpressionsWithAnonymousTypes()
        //{
        //    ISimpleService svc = Simply.Do[ConfigKey].Resolve<ISimpleService>();
        //    Assert.AreEqual(42, svc.Execute(x => new { asd = x * 2 }, 21).asd);
        //}
    }

    #region Samples
    public interface ISimpleService : IService
    {
        string GetString();
        int GetInt32();
        int Calculate(Expression<Func<int, int, int>> expr, int a, int b);
        T Execute<T>(Expression<Func<int, T>> expr, int a);

        byte[] GetByteArray(int size);
        int GetOverloadedMethod(int value);
        int GetOverloadedMethod(int value, int value2);

        string TestHeaderPassing();
        int TestHeaderPassingAndReturning();
        bool TestPassedIdentity();
        string this[int index] { get; }
        int SimpleProp { get; }

        bool TestExpression(EditableExpression expr, int value);
        bool TestSelfType(SelfType selfObject);
        bool CheckSameThread(int id);
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

    [Serializable]
    public class SelfType
    {
        public SelfType SelfProperty { get; set; }
    }

    public class SimpleService : MarshalByRefObject, ISimpleService, ISecondService
    {

        #region ISimpleService Members

        public string this[int index]
        {
            get
            {
                return index.ToString();
            }
        }

        public int SimpleProp { get { return 555; } }

        public string GetString()
        {
            return "whatever";
        }

        public int GetInt32()
        {
            return 42;
        }

        public int GetOverloadedMethod(int value)
        {
            return value;
        }

        public int GetOverloadedMethod(int value, int value2)
        {
            return value + value2;
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
            CallHeaders.Do["returnMe"] = a + 2;
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

        #region ISimpleService Members


        public bool TestExpression(EditableExpression expr, int value)
        {
            return ((Expression<Predicate<int>>)expr.ToExpression()).Compile()(value);
        }

        #endregion

        #region ISimpleService Members


        public bool TestSelfType(SelfType selfObject)
        {
            return object.ReferenceEquals(selfObject, selfObject.SelfProperty);
        }

        #endregion

        #region ISimpleService Members


        public bool CheckSameThread(int id)
        {
            return Thread.CurrentThread.ManagedThreadId == id;
        }

        #endregion

        #region ISimpleService Members


        public int Calculate(Expression<Func<int, int, int>> expr, int a, int b)
        {
            return expr.Compile()(a, b);
        }

        #endregion

        #region ISimpleService Members


        public T Execute<T>(Expression<Func<int, T>> expr, int a)
        {
            return expr.Compile()(a);
        }

        #endregion
    }

    #endregion
}
