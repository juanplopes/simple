using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.DynamicProxy;
using Simple.Reflection;

namespace Simple.Tests.DynamicProxy
{
    [TestFixture]
    public class MarshallableDynamicProxyFixture
    {
        public class SimpleServer : MarshalByRefObject
        {
            public int GetInt() { return 10; }
            public string GetString() { return "4"; }
            public void TestVoid(int param) { }
            public int TestIntParamReturn(int param) { return param; }
            public T TestGenericParamReturn<T>(T param) { 
                return param; 
            }
            public void TestRefAndOut(ref int p1, out int p2) { p2 = p1; p1 = 42; }
            public void TestException() { throw new InvalidCastException(); }
        }

        [Test]
        public void TestIntReturning()
        {
            SimpleServer server = new SimpleServer();
            Assert.AreEqual(10, server.GetInt());

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) => 42);
            Assert.AreEqual(42, server.GetInt());
        }

        [Test]
        public void TestStringReturning()
        {
            SimpleServer server = new SimpleServer();
            Assert.AreEqual("4", server.GetString());

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) => m.Invoke(o, p) + "2");
            Assert.AreEqual("42", server.GetString());
        }

        [Test]
        public void TestVoidReturning()
        {
            SimpleServer server = new SimpleServer();
            server.TestVoid(10);

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) => m.Invoke(o, p));
            server.TestVoid(10);
        }

        [Test]
        public void TestIntParamReturning()
        {
            SimpleServer server = new SimpleServer();
            Assert.AreEqual(10, server.TestIntParamReturn(10));

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) => (int)m.Invoke(o, p) + 1);
            Assert.AreEqual(11, server.TestIntParamReturn(10));
        }

        [Test]
        public void TestGenericParamReturning()
        {
            SimpleServer server = new SimpleServer();
            Assert.AreEqual(10, server.TestGenericParamReturn(10));

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) => (string)m.Invoke(o, p) + "2");
            Assert.AreEqual("42", server.TestGenericParamReturn("4"));
        }

        [Test, ExpectedException(typeof(InvalidCastException))]
        public void TestException()
        {
            SimpleServer server = new SimpleServer();

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) =>
                {
                    var invoker = InvokerFactory.Do.Create(m);
                    invoker.Invoke(o, p);
                    return null;
                });

            server.TestException();
        }


        [Test]
        public void TestRefAndOut()
        {
            SimpleServer server = new SimpleServer();
            int a = 10; int b;

            server.TestRefAndOut(ref a, out b);
            Assert.AreEqual(42, a);
            Assert.AreEqual(10, b);

            server = (SimpleServer)DynamicProxyFactory.Instance.CreateMarshallableProxy(server,
                (o, m, p) => { p[0] = 41; p[1] = 42; return null; });

            server.TestRefAndOut(ref a, out b);
            Assert.AreEqual(41, a);
            Assert.AreEqual(42, b);
        }


    }
}
