using System;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NUnit.Framework;
using SharpTestsEx;
using Simple.IO.Serialization;

namespace Simple.Tests.Serialization
{
    [TestFixture]
    public class BinarySerializerFixture : BaseSerializerFixture
    {
        protected override ISimpleSerializer GetSerializer<T>()
        {
            return SimpleSerializer.Binary();
        }

        protected override ISimpleStringSerializer GetStringSerializer<T>()
        {
            return SimpleSerializer.Binary();
        }

        [Test]
        public void TestSerializeLambda()
        {
            var ser = SimpleSerializer.Binary(new ExpressionSurrogateSelector());

            Expression<Func<int, int>> expr = x => x * 2;

            var newExpr = (Expression<Func<int, int>>)ser.Deserialize(ser.Serialize(expr));

            newExpr.Should().Not.Be(expr);
            newExpr.Compile()(21).Should().Be(42);
        }

        [Test]
        public void TestSerializeLambdaWithAnonymousTypes()
        {
            var ser = SimpleSerializer.Binary(new ExpressionSurrogateSelector());

            var expr = Expr(x => new { asd = x * 2 });

            var newExpr = (LambdaExpression)ser.Deserialize(ser.Serialize(expr));

            newExpr.Should().Not.Be(expr);
            var obj = newExpr.Compile().DynamicInvoke(21);
            obj.GetType().GetProperty("asd").GetValue(obj, null).Should().Be(42);
        }

        private Expression<Func<int, T>> Expr<T>(Expression<Func<int, T>> expr)
        {
            return expr;
        }
    }
}
