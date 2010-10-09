using System;
using System.Globalization;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Reflection;
using Moq;

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
            res.Should().Be(42);
        }

        [Test]
        public void TestStaticIntReturn()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestStaticInt"));
            object res = inv.Invoke(null);
            res.Should().Be(30);
        }

        [Test]
        public void TestStaticIntReturnWithParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestStaticIntString"));
            object res = inv.Invoke(null, "444");
            res.Should().Be(444);
        }

        [Test]
        public void TestStaticIntReturnWithTwoParams()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestStaticIntTwo"));
            object res = inv.Invoke(null, "444", 2);
            res.Should().Be(446);
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
            Func<int, int> func = x => x + x;
            var inv = InvokerFactory.Do.Create(func.Method);
            Assert.AreEqual(100, inv.Invoke(null, 50));
        }

        [Test]
        public void TestVoidReturn()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestVoid"));
            object res = inv.Invoke(new TestClass());
            res.Should().Be.Null();
        }

        [Test]
        public void TestStringReturn()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestString"));
            object res = inv.Invoke(new TestClass());
            res.Should().Be("whatever");
        }

        [Test]
        public void TestIntParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestIntParam"));
            object res = inv.Invoke(new TestClass(), 42);

            res.Should().Be(42);
        }

        [Test]
        public void TestOutParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestOutParam"));
            var objs = new object[] { 10, null };

            object res = inv.Invoke(new TestClass(), objs);
            objs[1].Should().Be("10");
            res.Should().Be("whatever");
        }

        [Test]
        public void TestRefParam()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClass).GetMethod("TestRefParam"));
            object[] p = { 42 };
            object res = inv.Invoke(new TestClass(), p);
            res.Should().Be("42");
            p[0].Should().Be(43);
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
            res.Should().Be(42);
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
            res.Should().Be("42");
        }

        [Test]
        public void TestCreateInstance1()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClassConstructors)
                .GetConstructor(new[] { typeof(string), typeof(string) }));

            var obj = inv(null, "A", "B") as TestClassConstructors;

            obj.param1.Should().Be("A");
            obj.param2.Should().Be("B");
        }

        [Test, Explicit("The world is cruel, my friend")]
        public void WillNotBeAbleToCallPropertySetToWrongType()
        {
            var inv = InvokerFactory.Do.Create(typeof(ClassB)
                .GetProperty("Prop").GetSetMethod());
            var a = new ClassA();

            inv(a, "asd");

            a.Prop.GetType().Should().Be(typeof(PropClass));
        }

        [Test]
        public void TestCreateInstance2()
        {
            var inv = InvokerFactory.Do.Create(typeof(TestClassConstructors)
                .GetConstructor(new[] { typeof(int), typeof(int) }));

            var obj = inv(null, 1, 2) as TestClassConstructors;

            obj.param1.Should().Be(1);
            obj.param2.Should().Be(2);
        }


        class PropClass
        {

        }

        class ClassA
        {
            public PropClass Prop { get; set; }
        }
        class ClassB
        {
            public string Prop { get; set; }
        }

        class TestClassConstructors
        {
            public object param1 = null;
            public object param2 = null;
            public TestClassConstructors(string a, string b) { param1 = a; param2 = b; }
            public TestClassConstructors(int a, int b) { param1 = a; param2 = b; }
            public TestClassConstructors(string a) { param1 = a; }
        }

        class TestClass
        {
            public static int TestStaticInt() { return 30; }
            public static int TestStaticIntString(string a) { return int.Parse(a); }
            public static int TestStaticIntTwo(string a, int b) { return int.Parse(a) + b; }
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
