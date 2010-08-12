using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.Expressions.Editable;

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

            // x => x > 2 && invoke(y => y < 3, x);
            p1 = Expression.Lambda<Predicate<int>>(Expression.AndAlso(p1.Body, Expression.Invoke(p2, p1.Parameters.ToArray())), p1.Parameters.ToArray());
            p1.Nodes().OfType<ParameterExpression>().Count().Should().Be(5);
            p1.Nodes().OfType<InvocationExpression>().Count().Should().Be(1);

            // x => x > 2 && x < 3
            p1 = InvocationExpander.Expand(p1);
            p1.Nodes().OfType<ParameterExpression>().Count().Should().Be(3);
            p1.Nodes().OfType<InvocationExpression>().Count().Should().Be(0);

            //expected result
            Expression<Predicate<int>> pExp = x => x > 2 && x < 3;
            p1.ToString().Should().Be(pExp.ToString());

        }

        [Test]
        public void CanExpandTwoVariablePredicate()
        {
            Expression<Func<int, int, bool>> p1 = (x, y) => x > y;
            Expression<Func<int, int, bool>> p2 = (y, x) => x > y;

            // (x, y) => x > y || invoke((y, x) => x > y, x, y);
            p1 = Expression.Lambda<Func<int, int, bool>>(Expression.OrElse(p1.Body, Expression.Invoke(p2, p1.Parameters.ToArray())), p1.Parameters.ToArray());
            p1.Nodes().OfType<ParameterExpression>().Count().Should().Be(10);
            p1.Nodes().OfType<InvocationExpression>().Count().Should().Be(1);

            // (x, y) => x > y || y > x;
            p1 = InvocationExpander.Expand(p1);
            p1.Nodes().OfType<ParameterExpression>().Count().Should().Be(6);
            p1.Nodes().OfType<InvocationExpression>().Count().Should().Be(0);

            //expected result
            Expression<Func<int, int, bool>> pExp = (x, y) => x > y || y > x;
            p1.ToString().Should().Be(pExp.ToString());

        }
    }
}
