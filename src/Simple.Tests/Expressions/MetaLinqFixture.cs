using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.Expressions.Editable;
using Simple.IO.Serialization;
using System.Collections;

namespace Simple.Tests.Expressions
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
            var expr1 = EditableExpression.Create(Funcletizer.PartialEval(expr));
            byte[] data = serializer.Serialize(expr1);
            EditableExpression expr2 = (EditableExpression)serializer.Deserialize(data);
            T func2 = ((Expression<T>)expr2.ToExpression()).Compile();

            var value1 = howToCall(func2);
            var value2 = howToCall(expr.Compile());

            Assert.AreEqual(value2, value1);
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
        public void TestConditionalAssertionWithNullableFields()
        {
            TestIt((decimal? x, decimal y) => x > y, 2, 3);
        }

        [Test]
        public void TestConditionalAssertionWithNullableMemberAccess()
        {
            var a1 = DateTime.Now;
            var a2 = a1.AddDays(-2);
            var dd = new SerializableClassWithNullableField() { MyDateTime = a2 };
            TestIt(x => x.MyDateTime < a1, dd);
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
        public void TestInnerExpressionSerialization()
        {
            Expression<Func<IQueryable<int>, IQueryable<int>>> expr = q => q.Where(x => x % 2 == 0);
            TestIt(expr, new[] { 1, 2, 3, 4 }.AsQueryable());
        }

        [Test]
        public void TestStaticMethodCall()
        {
            TestIt(x => int.Parse(x), "123");
        }

        [Test]
        public void TestGenericMethodCall()
        {
            TestIt(x => x.ConvertTo<double, string>(), new SerializableClass() { MyInt = 2 });
        }

        [Test]
        public void TestGenericStaticMethodCall()
        {
            TestIt(x => StaticClass.ConvertTo<int, string>(x), 2);
        }

        [Test]
        public void TestTypeSerialization()
        {
            var type = typeof(Expression<Func<int, bool>>);
            var bytes = SimpleSerializer.Binary().Serialize(type);
            var type2 = (Type)SimpleSerializer.Binary().Deserialize(bytes);
            type2.Should().Be(type);
        }

        [Test]
        public void TestMethodSerialization()
        {
            var method = typeof(Queryable).GetMember("Where").OfType<MethodInfo>().Where(x => x.GetParameters().Length == 2).First();
            var bytes = SimpleSerializer.Binary().Serialize(method);
            var method2 = (MethodInfo)SimpleSerializer.Binary().Deserialize(bytes);
            method2.Should().Be(method);
        }

        [Test]
        public void TestGeneratedQuoteSerialization()
        {
            Expression<Func<int, int>> original = x => x * 2;
            var expr1 = EditableExpression.Create(Expression.Quote(original));

            var bytes = SimpleSerializer.Binary().Serialize(expr1);
            var expr2 = (EditableExpression)SimpleSerializer.Binary().Deserialize(bytes);
            expr2.Should().Not.Be.Null();
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

        [Serializable]
        public class SerializableClass
        {
            public int MyInt { get; set; }

            public Q ConvertTo<T, Q>()
            {
                var t = (T)Convert.ChangeType(this.MyInt, typeof(T));
                return (Q)Convert.ChangeType(t, typeof(Q));
            }
        }

        public static class StaticClass
        {
            public static Q ConvertTo<T, Q>(T value)
            {
                return (Q)Convert.ChangeType(value, typeof(Q));
            }
        }

        [Serializable]
        public class SerializableClassWithNullableField
        {
            public DateTime? MyDateTime { get; set; }
        }

        public class NonSerialiableClass
        {
            public int NonInt { get; set; }
        }

        public class NonSerialzableLevel2
        {
            public NonSerialiableClass ItsStillNotSerialiable { get; set; }
        }

        public class SerialiableLevel2Tricky
        {
            public NonSerialiableClass ItsTricky { get; set; }
        }

        [Test]
        public void TestSerializableStackReference()
        {
            SerializableClass c = new SerializableClass() { MyInt = 2 };

            TestIt(y => new int[c.MyInt + y], 4);
        }

        [Test]
        public void TestNonSerializableStackReference()
        {
            NonSerialiableClass c = new NonSerialiableClass() { NonInt = 2 };

            TestIt(y => new int[c.NonInt + y], 4);
        }

        [Test]
        public void TestSerialiableParameter()
        {
            TestIt(y => y.MyInt == 42, new SerializableClass() { MyInt = 42 });
        }

        [Test]
        public void TestNonSerialiableParameter()
        {
            TestIt(y => y.NonInt == 52, new NonSerialiableClass() { NonInt = 52 });
        }

        [Test]
        public void TestNonSerialiableParameterLevel2()
        {
            TestIt(y => y.ItsStillNotSerialiable.NonInt == 52, new NonSerialzableLevel2()
            {
                ItsStillNotSerialiable = new NonSerialiableClass()
                {
                    NonInt = 52
                }
            });
        }

        [Test]
        public void TestSerialiableParameterLevel2Tricky()
        {
            TestIt(y => y.ItsTricky.NonInt == 52, new SerialiableLevel2Tricky()
            {
                ItsTricky = new NonSerialiableClass()
                {
                    NonInt = 52
                }
            });
        }


    }
}
