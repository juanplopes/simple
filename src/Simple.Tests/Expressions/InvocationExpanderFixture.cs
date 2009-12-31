using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Expressions;

namespace Simple.Tests.Expressions
{
    [TestFixture]
    public class InvocationExpanderFixture
    {
        [Test]
        public void CanExpandSimplePredicate()
        {
            Expression<Predicate<int>> p1 = x => x > 2;
            Expression<Predicate<int>> p2 = y => y < 3;

            Expression<Predicate<int>> pExp = x => x > 2 && x < 3;

            // x => x > 2 && invoke(y => y < 3, x);
            p1 = Expression.Lambda<Predicate<int>>(Expression.AndAlso(p1.Body, Expression.Invoke(p2, p1.Parameters.ToArray())), p1.Parameters.ToArray());
            Assert.AreEqual(5, p1.Nodes().OfType<ParameterExpression>().Count());
            Assert.AreEqual(1, p1.Nodes().OfType<InvocationExpression>().Count());

            // x => x > 2 && x < 3
            p1 = InvocationExpander.Expand(p1);
            Assert.AreEqual(3, p1.Nodes().OfType<ParameterExpression>().Count());
            Assert.AreEqual(0, p1.Nodes().OfType<InvocationExpression>().Count());
            Assert.AreEqual(pExp.ToString(), p1.ToString());

        }

        [Test]
        public void CanExpandTwoVariablePredicate()
        {
            Expression<Func<int, int, bool>> p1 = (x, y) => x > y;
            Expression<Func<int, int, bool>> p2 = (y, x) => x > y;

            Expression<Func<int, int, bool>> pExp = (x, y) => x > y || y > x;

            // (x, y) => x > y || invoke((y, x) => x > y, x, y);
            p1 = Expression.Lambda<Func<int, int, bool>>(Expression.OrElse(p1.Body, Expression.Invoke(p2, p1.Parameters.ToArray())), p1.Parameters.ToArray());
            Assert.AreEqual(10, p1.Nodes().OfType<ParameterExpression>().Count());
            Assert.AreEqual(1, p1.Nodes().OfType<InvocationExpression>().Count());

            // (x, y) => x > y || y > x;
            p1 = InvocationExpander.Expand(p1);
            Assert.AreEqual(6, p1.Nodes().OfType<ParameterExpression>().Count());
            Assert.AreEqual(0, p1.Nodes().OfType<InvocationExpression>().Count());
            Assert.AreEqual(pExp.ToString(), p1.ToString());

        }
    }
}
