using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.IO.Serialization;
using Simple.Tests.Resources;

namespace Simple.Tests.Entities
{
    public class OrderByFixture
    {
        [Test]
        public void CanOrderByFirstColumn()
        {
            var spec = Customer.Do.OrderBy(x => x.Address);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.OrderBy(x => x.Address)");
        }

        [Test]
        public void CanOrderByTwoColumns()
        {
            var spec = Customer.Do.OrderBy(x => x.Address).ThenBy(x => x.ContactName);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.OrderBy(x => x.Address).ThenBy(x => x.ContactName)",
                queryable.Expression.ToString());
        }

        [Test]
        public void CanOrderByFirstColumnDesc()
        {
            var spec = Customer.Do.OrderByDesc(x => x.Address);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.OrderByDescending(x => x.Address)");
        }

        [Test]
        public void CanOrderByTwoColumnsWithSecondDescending()
        {
            var spec = Customer.Do.OrderBy(x => x.Address).ThenByDesc(x => x.ContactName);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.OrderBy(x => x.Address).ThenByDescending(x => x.ContactName)",
                queryable.Expression.ToString());
        }

        [Test]
        public void CanSerializeOrderByWithAllAsc()
        {
            var spec = Customer.Do.OrderBy(x => x.Address).ThenBy(x => x.ContactName);
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);
        }

        [Test]
        public void CanSerializeOrderByWithAllDesc()
        {
            var spec = Customer.Do.OrderByDesc(x => x.Address).ThenByDesc(x => x.ContactName);
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);
        }
    }
}
