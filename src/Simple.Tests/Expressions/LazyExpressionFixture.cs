using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions.Editable;
using Simple.IO.Serialization;

namespace Simple.Tests.Expressions
{
    public class LazyExpressionFixture
    {
        [Test]
        public void WhenSerializingSimpleExpressionItWorksWell()
        {
            var lazy = new LazyExpression<Func<string, int>>(x => int.Parse(x));
            var real = lazy.Real;

            var newLazy = SimpleSerializer.Binary().RoundTrip(lazy);
            
            Assert.IsFalse(newLazy.IsRealActivated);
            Assert.IsTrue(newLazy.IsProxyActivated);
            var newReal = newLazy.Real;
            Assert.IsTrue(newLazy.IsRealActivated);
            Assert.IsTrue(newLazy.IsProxyActivated);


            Assert.AreNotSame(real, newReal);
        }

        [Test]
        public void WhenSerializingNullExpressionItWorksWell()
        {
            var lazy = new LazyExpression<Func<string, int>>(null);
            var real = lazy.Real;

            var newLazy = SimpleSerializer.Binary().RoundTrip(lazy);

            Assert.IsFalse(newLazy.IsRealActivated);
            Assert.IsFalse(newLazy.IsProxyActivated);
            var newReal = newLazy.Real;
            Assert.IsTrue(newLazy.IsRealActivated);
            Assert.IsFalse(newLazy.IsProxyActivated);


            newReal.Should().Be(real);
       }
    }
}
