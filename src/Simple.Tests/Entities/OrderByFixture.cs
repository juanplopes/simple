using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.Resources;
using Simple.Entities;
using Simple.Expressions;
using NUnit.Framework;
using Simple.IO.Serialization;
using System.Linq.Expressions;
using System.Reflection;
using Simple.Reflection;
using Simple.Common;

namespace Simple.Tests.Entities
{
    public class OrderByFixture
    {
        [Test]
        public void CanOrderByFirstColumn()
        {
            var spec = Customer.Do.OrderBy(x => x.Address);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.OrderBy(x => x.Address)", queryable.Expression.ToString());
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
            Assert.AreEqual("q.OrderByDescending(x => x.Address)", queryable.Expression.ToString());
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
