using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Expressions;
using Simple.IO.Serialization;
using Simple.Expressions.Editable;

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


            Assert.AreEqual(real, newReal);
       }
    }
}
