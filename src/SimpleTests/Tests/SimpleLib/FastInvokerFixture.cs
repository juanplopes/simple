using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class FastInvokerFixture
    {
        [Test]
        public void TestIntReturn()
        {
            FastInvoker inv = new FastInvoker(typeof(TestClass).GetMethod("TestInt"));
            object res = inv.Invoke(new TestClass(), new object[] { });
            Assert.AreEqual(42, res);
        }
        [Test]
        public void TestVoidReturn()
        {
            FastInvoker inv = new FastInvoker(typeof(TestClass).GetMethod("TestVoid"));
            object res = inv.Invoke(new TestClass(), new object[] { });
            Assert.IsNull(res);
        }

        [Test]
        public void TestStringReturn()
        {
            FastInvoker inv = new FastInvoker(typeof(TestClass).GetMethod("TestString"));
            object res = inv.Invoke(new TestClass(), new object[] { });
            Assert.AreEqual("whatever", res);
        }

        [Test]
        public void TestIntParam()
        {
            FastInvoker inv = new FastInvoker(typeof(TestClass).GetMethod("TestIntParam"));
            object res = inv.Invoke(new TestClass(), new object[] { 42 });
            Assert.AreEqual(42, res);
        }

        [Test, Ignore]
        public void TestOutParam()
        {
            FastInvoker inv = new FastInvoker(typeof(TestClass).GetMethod("TestOutParam2"));
            string s = null;
            object res = inv.Invoke(new TestClass(), new object[] { 10, s });
            Assert.AreEqual("10", s);
            Assert.AreEqual("whatever", res);
        }

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "AAA")]
        public void TestException()
        {
            FastInvoker inv = new FastInvoker(typeof(TestClass).GetMethod("TestException"));
            object res = inv.Invoke(new TestClass());
        }


        class TestClass
        {
            public int TestInt() { return 42; }
            public void TestVoid() { }
            public string TestString() { return "whatever"; }
            public int TestIntParam(int p) { return p; }
            public string TestOutParam(out string s) { s = "42"; return "whatever"; }
            public string TestOutParam2(int i, out string s) { s = i.ToString(); return "whatever"; }
            public string TestRefParam(ref int i) { string s = i.ToString(); i = 42; return s; }

            public void TestException() { throw new ArgumentException("AAA"); }
        }
    }
}
