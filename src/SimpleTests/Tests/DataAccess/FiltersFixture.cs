using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Simple.Filters;
using Simple.DataAccess;
using NHCrit = NHibernate.Criterion;
using NHibernate.Linq;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class FiltersFixture
    {
        protected void AssertSimpleFilter(string filterExpression)
        {
            PropertyName test = new PropertyName("SomeProperty");
            NHCrit.ICriterion crit = CriteriaHelper.GetCriterion(new SimpleExpression(test, "someValue", filterExpression));
            Assert.IsTrue(crit is NHCrit.SimpleExpression, "Wrong ICriterion type. Filter {0}, Found {1}", filterExpression, (crit==null?"NULL":crit.GetType().Name));
            Assert.AreEqual(((NHCrit.SimpleExpression)crit).PropertyName, test.Name);
            Assert.AreEqual(((NHCrit.SimpleExpression)crit).Value, "someValue");
        }

        [Test]
        public void SimpleFilters_Eq()
        {
            AssertSimpleFilter(SimpleExpression.EqualsExpression);
        }
        [Test]
        public void SimpleFilters_Lt()
        {
            AssertSimpleFilter(SimpleExpression.LesserThanExpression);
        }
        [Test]
        public void SimpleFilters_Gt()
        {
            AssertSimpleFilter(SimpleExpression.GreaterThanExpression);
        }
        [Test]
        public void SimpleFilters_LtEq()
        {
            AssertSimpleFilter(SimpleExpression.LesserThanOrEqualsExpression);
        }
        [Test]
        public void SimpleFilters_GtEq()
        {
            AssertSimpleFilter(SimpleExpression.GreaterThanOrEqualsExpression);
        }



    }
}
