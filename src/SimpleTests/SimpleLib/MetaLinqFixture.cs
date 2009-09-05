using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using System.Xml.Serialization;
using Simple.Common;
using Simple.IO;
using Simple.IO.Serialization;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class MetaLinqFixture
    {
        public void TestIt<TRet>(Expression<Func<TRet>> expr)
        {
            TestItInternal<Func<TRet>, TRet>(expr, f => f());
        }

        public void TestIt<T1, TRet>(Expression<Func<T1, TRet>> expr, T1 t1)
        {
            TestItInternal<Func<T1, TRet>, TRet>(expr, f => f(t1));
        }


        public void TestIt<T1, T2, TRet>(Expression<Func<T1, T2, TRet>> expr, T1 t1, T2 t2)
        {
            TestItInternal<Func<T1, T2, TRet>, TRet>(expr, f => f(t1, t2));
        }

        public void TestItInternal<T, TRet>(Expression<T> expr, Func<T, TRet> howToCall)
        {
            TestItInternalInternal<T, TRet>(expr, howToCall,
                SimpleSerializer.Binary());

            TestItInternalInternal<T, TRet>(expr, howToCall,
                SimpleSerializer.NetDataContract());
        }

        public void TestItInternalInternal<T, TRet>(Expression<T> expr, Func<T, TRet> howToCall, ISimpleSerializer serializer)
        {
            var expr1 = EditableExpression.CreateEditableExpression(expr);
            byte[] data = serializer.Serialize(expr1);
            EditableExpression expr2 = (EditableExpression)serializer.Deserialize(data);
            T func2 = ((Expression<T>)expr2.ToExpression()).Compile();
            Assert.AreEqual(howToCall(expr.Compile()), howToCall(func2));
        }

        [Test]
        public void TestConstantExpression()
        {
            TestIt(() => 42);
        }

        [Test]
        public void TestAddExpression()
        {
            TestIt((x, y) => x + y, 2, 3);
        }

        [Test]
        public void TestConstruction()
        {
            TestIt((x, y) => new string(x, y), 'a', 3);
        }

        [Test]
        public void TestConditionalAssertion()
        {
            TestIt((x, y) => x - y == y && x > y, 2, 3);
        }

        [Test]
        public void TestMethodCall()
        {
            TestIt((x, y) => int.Parse(x) == y, "3", 3);
        }

        [Test]
        public void TestStackReference()
        {
            int x = 42;
            TestIt(y => x == y, 41);
        }

        [Test]
        public void TestStackMethodCall()
        {
            int x = 42;
            TestIt(y => x.ToString() == y, "41");
        }
        [Test]
        public void TestStackPropertyCall()
        {
            var x = new[] { 1, 2, 3 };
            TestIt(y => y == x.Length, 412);
        }

        [Test]
        public void TestArrayCreation()
        {
            int h = 10;
            TestIt(y => new int[h + y], 5);
        }

    }
}
