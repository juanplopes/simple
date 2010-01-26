using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;
using NHibernate.Criterion;
using System.Linq.Expressions;
using System.Runtime.Serialization;

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

            Assert.AreNotEqual(expr, newExpr);
            Assert.AreEqual(42, newExpr.Compile()(21));
        }
    }
}
