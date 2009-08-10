using System;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using NHibernate.Util;

namespace Simple.Tests.Experiences
{
    [TestFixture]
    public class LambdaFixture
    {
        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void TestSerializeExpression()
        {
            Expression<Predicate<int>> lambda =
                x => x == 2 && x == 3;

            SerializationHelper.Serialize(lambda);
        }

        [Test]
        public void TestSerializationWithILBytes()
        {
            Func<int, bool> func = x => x == 42;
            byte[] body = SerializationHelper.Serialize(func);
            var newFunc = (Func<int, bool>)SerializationHelper.Deserialize(body);

            int x1 = 41, x2 = 42;

            Assert.IsFalse(func(x1));
            Assert.IsFalse(newFunc(x1));

            Assert.IsTrue(func(x2));
            Assert.IsTrue(func(x2));
        }

        [Test, ExpectedException(typeof(SerializationException))]
        public void TestSerializationWithStackReference()
        {
            int hh = 42;

            Func<int, bool> func = x => x == hh;
            byte[] body = SerializationHelper.Serialize(func);
            var newFunc = (Func<int, bool>)SerializationHelper.Deserialize(body);

            int x1 = 41, x2 = 42;

            Assert.IsFalse(func(x1));
            Assert.IsFalse(newFunc(x1));

            Assert.IsTrue(func(x2));
            Assert.IsTrue(func(x2));
        }
    }
}
