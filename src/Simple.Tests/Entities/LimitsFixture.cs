using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.IO.Serialization;
using Simple.Tests.Resources;

namespace Simple.Tests.Entities
{
    public class LimitsFixture
    {
        [Test]
        public void CanOnlySkip()
        {
            var spec = Customer.Do.Skip(10);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.Skip(10)");
        }

        [Test]
        public void CanOnlyTake()
        {
            var spec = Customer.Do.Take(10);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.Take(10)");

        }

        [Test]
        public void CanSkipAndTake()
        {
            var spec = Customer.Do.Skip(10).Take(11);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.Skip(10).Take(11)");
        }

        [Test]
        public void CanTakeAndSkip()
        {
            var spec = Customer.Do.Take(10).Skip(11);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.Take(10).Skip(11)");

        }

        [Test]
        public void CanSerializeLimits()
        {
            var spec = Customer.Do.Take(10).Skip(11);
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);
        }
    }
}
