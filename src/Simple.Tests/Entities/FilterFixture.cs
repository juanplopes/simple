using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.Resources;
using Simple.Entities;
using Simple.Expressions;
using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.Entities
{
    public class FilterFixture
    {
        [Test]
        public void CanSimpleFilter()
        {
            var spec = Customer.Do.Filter(x => x.CompanyName == "asd");
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.Where(x => (x.CompanyName = \"asd\"))", queryable.Expression.ToString());
        }

        [Test]
        public void CanFilterWithStackReference()
        {
            string value = "asd";
            var spec = Customer.Do.Filter(x => x.CompanyName == value);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.Where(x => (x.CompanyName = \"asd\"))", queryable.Expression.ToString());
        }

        [Test]
        public void CanSerializeFilter()
        {
            var spec = Customer.Do.Filter(x => x.CompanyName == "asd");
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);
        }

        [Test]
        public void CanSerializeFilterWithStackReference()
        {
            string value = "asd";
            var spec = Customer.Do.Filter(x => x.CompanyName == value);
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);
        }
    }
}
