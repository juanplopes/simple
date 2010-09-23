using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.IO.Serialization;
using Simple.Tests.Resources;
using System.Linq;

namespace Simple.Tests.Entities
{
    public class ExpressionFixture
    {
        [Test]
        public void CanSimpleFilter()
        {
            var spec = Customer.Do.Expr(q => q.Where(x => x.CompanyName == "asd").Skip(2).Take(10));
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.Where(x => (x.CompanyName = \"asd\")).Skip(2).Take(10)");
        }

        [Test]
        public void CanFilterWithStackReference()
        {
            string value = "asd";
            int a = 2, b = 10;
            var spec = Customer.Do.Expr(q => q.Where(x => x.CompanyName == value).Skip(a).Take(b));
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            queryable.Expression.ToString().Should().Be("q.Where(x => (x.CompanyName = \"asd\")).Skip(2).Take(10)");
        }

        [Test]
        public void CanSerializeFilter()
        {
            var spec = Customer.Do.Expr(q => q.Where(x => x.CompanyName == "asd"));
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);

            var queryable1 = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            var queryable2 = new EmptyQueryable<Customer>("q").ApplySpecs(spec2);

            queryable2.Expression.ToString().Should().Be(queryable1.Expression.ToString());
        }

        [Test]
        public void CanSerializeFilterWithStackReference()
        {
            string value = "asd";
            var spec = Customer.Do.Expr(q => q.Where(x => x.CompanyName == value));
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);

            var queryable1 = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            var queryable2 = new EmptyQueryable<Customer>("q").ApplySpecs(spec2);

            queryable2.Expression.ToString().Should().Be(queryable1.Expression.ToString());
        }
    }
}
