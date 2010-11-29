using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using System.Linq.Expressions;

namespace Simple.Tests.Expressions
{
    [TestFixture]
    public class PredicateBuilderFixture
    {
        [Test]
        public void CanApplyAndToConstantExpression()
        {
            var expr = PredicateBuilder.True<string>();
            expr = expr.And(x => x.Length == 2);
            expr.ToString().Should().Be("f => (True && (f.Length = 2))");
        }

        [Test]
        public void CanApplyOrToConstantExpression()
        {
            var expr = PredicateBuilder.False<string>();
            expr = expr.Or(x => x.Length == 2);
            expr.ToString().Should().Be("f => (False || (f.Length = 2))");
        }

        [Test]
        public void CanApplyOrToNull_ItConvertsNullToFalse()
        {
            var expr = PredicateBuilder.Or<string>(null, x => x.Length == 2);
            expr.ToString().Should().Be("f => (False || (f.Length = 2))");
        }

        [Test]
        public void CanApplyAndToNull_ItConvertsNullToTrue()
        {
            var expr = PredicateBuilder.And<string>(null, x => x.Length == 2);
            expr.ToString().Should().Be("f => (True && (f.Length = 2))");
        }

        [Test]
        public void CanApplyOrToConstantExpressionWithCustomParameter()
        {
            var expr = PredicateBuilder.True<string>(Expression.Parameter(typeof(string), "p"));
            expr = expr.And(x => x.Length == 2);
            expr.ToString().Should().Be("p => (True && (p.Length = 2))");
        }

        [Test]
        public void CanApplyAndToConstantExpressionWithCustomParameter()
        {
            var expr = PredicateBuilder.False<string>(Expression.Parameter(typeof(string), "p"));
            expr = expr.Or(x => x.Length == 2);
            expr.ToString().Should().Be("p => (False || (p.Length = 2))");
        }
    }
}
