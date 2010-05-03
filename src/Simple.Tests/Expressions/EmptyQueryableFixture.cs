using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Expressions;
using System.Linq.Expressions;
using Simple.Tests.Resources;

namespace Simple.Tests.Expressions
{
    public class EmptyQueryableFixture
    {
        [Test]
        public void CanRecordLinqExpressions()
        {
            var queryable = new EmptyQueryable<int>(Parameter<int>("q"));
            var newQuery = queryable.Where(x => x > 2);

            Assert.AreEqual("q.Where(x => (x > 2))", newQuery.Expression.ToString());
        }

        [Test]
        public void CanRecordTwoLevelLinqExpressions()
        {
            var queryable = new EmptyQueryable<int>(Parameter<int>("q"));
            var newQuery = queryable.Where(x => x > 2).Distinct();

            Assert.AreEqual("q.Where(x => (x > 2)).Distinct()", newQuery.Expression.ToString());
        }
        
        [Test]
        public void CanRecordTwoLevelLinqExpressionsWithProjection()
        {
            var queryable = new EmptyQueryable<DateTime>(Parameter<DateTime>("q"));
            var newQuery = queryable.Where(x => x > DateTime.Now).Distinct().Select(x => x.Day);

            Assert.AreEqual("q.Where(x => (x > DateTime.Now)).Distinct().Select(x => x.Day)", newQuery.Expression.ToString());
        }

        [Test]
        public void CanRecordTwoLevelLinqExpressionsWithAggregate()
        {
            var queryable = new EmptyQueryable<DateTime>(Parameter<DateTime>("q"));
            var newQuery = queryable.Where(x => x > DateTime.Now).Distinct().GroupBy(x => x.Day).Select(x => x.Max());

            Assert.AreEqual("q.Where(x => (x > DateTime.Now)).Distinct().GroupBy(x => x.Day).Select(x => x.Max())",
                newQuery.Expression.ToString());
        }

        [Test]
        public void CanRecordLinqExpressionsWithOrderBy()
        {
            var queryable = new EmptyQueryable<DateTime>(Parameter<DateTime>("q"));
            var newQuery = queryable.OrderBy(x => x.Day);

            Assert.AreEqual("q.OrderBy(x => x.Day)",
                newQuery.Expression.ToString());
        }

        public ParameterExpression Parameter<T>(string name)
        {
            return Expression.Parameter(typeof(IQueryable<T>), name);
        }
    }
}
