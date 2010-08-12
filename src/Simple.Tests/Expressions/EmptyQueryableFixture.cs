using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;

namespace Simple.Tests.Expressions
{
    public class EmptyQueryableFixture
    {
        [Test]
        public void CanRecordLinqExpressions()
        {
            var queryable = new EmptyQueryable<int>(Parameter<int>("q"));
            var newQuery = queryable.Where(x => x > 2);

            newQuery.Expression.ToString().Should().Be("q.Where(x => (x > 2))");
        }

        [Test]
        public void CanRecordTwoLevelLinqExpressions()
        {
            var queryable = new EmptyQueryable<int>(Parameter<int>("q"));
            var newQuery = queryable.Where(x => x > 2).Distinct();

            newQuery.Expression.ToString().Should().Be("q.Where(x => (x > 2)).Distinct()");
        }
        
        [Test]
        public void CanRecordTwoLevelLinqExpressionsWithProjection()
        {
            var queryable = new EmptyQueryable<DateTime>(Parameter<DateTime>("q"));
            var newQuery = queryable.Where(x => x > DateTime.Now).Distinct().Select(x => x.Day);

            newQuery.Expression.ToString().Should().Be("q.Where(x => (x > DateTime.Now)).Distinct().Select(x => x.Day)");
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
