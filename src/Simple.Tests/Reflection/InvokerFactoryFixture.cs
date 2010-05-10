using System;
using System.Globalization;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class InvokerFactoryFixture
    {
        [Test]
        public void TestIntReturn()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestInt"));
            object res = inv.Invoke(new TestClass());
            Assert.AreEqual(42, res);
        }

        [Test]
        public void TestLambdaVoid()
        {
            Action action = () => { };
            var inv = InvokerFactory.Do.Create(action.Method);
            inv.Invoke(null);
        }

        [Test]
        public void TestTypedLambda()
        {
            Func<int,int> func = x => x+x ;
            var inv = InvokerFactory.Do.Create(func.Method);
            Assert.AreEqual(100, inv.Invoke(null, 50));
        }

        [Test]
        public void TestVoidReturn()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestVoid"));
            object res = inv.Invoke(new TestClass());
            Assert.IsNull(res);
        }

        [Test]
        public void TestStringReturn()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestString"));
            object res = inv.Invoke(new TestClass());
            Assert.AreEqual("whatever", res);
        }

        [Test]
        public void TestIntParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestIntParam"));
            object res = inv.Invoke(new TestClass(), 42);

            Assert.AreEqual(42, res);
        }

        [Test]
        public void TestOutParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestOutParam"));
            var objs = new object[] { 10, null };

            object res = inv.Invoke(new TestClass(), objs);
            Assert.AreEqual("10", objs[1]);
            Assert.AreEqual("whatever", res);
        }

        [Test]
        public void TestRefParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestRefParam"));
            object[] p = { 42 };
            object res = inv.Invoke(new TestClass(), p);
            Assert.AreEqual("42", res);
            Assert.AreEqual(43, p[0]);
        }

        [Test, ExpectedException(typeof(InvalidCastException))]
        public void TestWrongTypeSignature()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestIntParam"));
            inv.Invoke(new TestClass(), new object[] { "teste" });
        }

        [Test]
        public void TestMoreArgumentsSignature()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestIntParam"));
            inv.Invoke(new TestClass(), new object[] { 10, "teste" });
        }

        [Test, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestLessArgumentsSignature()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestIntParam"));
            inv.Invoke(new TestClass(), new object[] { });
        }

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "AAA")]
        public void TestException()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestException"));
            object res = inv.Invoke(new TestClass());
        }

        [Test]
        public void TestGenerics()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass)
                .GetMethod("TestGenerics").MakeGenericMethod(typeof(string)));
            object res = inv.Invoke(new TestClass(), "42");
            Assert.AreEqual(42, res);
        }


        [Test]
        public void TestGenericsVoid()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass)
                .GetMethod("TestGenericsVoid").MakeGenericMethod(typeof(string)));
            object res = inv.Invoke(new TestClass(), "42");
        }

        [Test]
        public void TestParams()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass)
                .GetMethod("TestParams"));

            object res = inv.Invoke(new TestClass(), 4, new string[] { "1", "2", "3" });
            Assert.AreEqual("42", res);
        }

        class TestClass
        {
            public int TestInt() { return 42; }
            public void TestVoid() { }
            public string TestString() { return "whatever"; }
            public int TestIntParam(int p) { return p; }
            public string TestOutParam(int i, out string s)
            {
                s = i.ToString();
                return "whatever";
            }
            public string TestRefParam(ref int i) { string s = i.ToString(); i++; return s; }

            public void TestException() { throw new ArgumentException("AAA"); }

            public int TestGenerics<T>(T obj)
                where T : IConvertible
            {
                return obj.ToInt32(CultureInfo.InvariantCulture);
            }

            public void TestGenericsVoid<T>(T obj)
                where T : IConvertible
            {
                obj.ToInt32(CultureInfo.InvariantCulture);
            }

            public string TestParams(int value, params string[] values)
            {
                return value.ToString() + values[values.Length - 2];
            }
        }
    }
}
