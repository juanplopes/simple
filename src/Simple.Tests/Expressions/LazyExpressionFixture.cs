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
            
            newLazy.IsRealActivated.Should().Be.False();
            newLazy.IsProxyActivated.Should().Be.True();
            var newReal = newLazy.Real;
            newLazy.IsRealActivated.Should().Be.True();
            newLazy.IsProxyActivated.Should().Be.True();


            Assert.AreNotSame(real, newReal);
        }

        [Test]
        public void WhenSerializingNullExpressionItWorksWell()
        {
            var lazy = new LazyExpression<Func<string, int>>(null);
            var real = lazy.Real;

            var newLazy = SimpleSerializer.Binary().RoundTrip(lazy);

            newLazy.IsRealActivated.Should().Be.False();
            newLazy.IsProxyActivated.Should().Be.False();
            var newReal = newLazy.Real;
            newLazy.IsRealActivated.Should().Be.True();
            newLazy.IsProxyActivated.Should().Be.False();


            newReal.Should().Be(real);
       }
    }
}
