using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Simple.Expressions;
using Simple.Expressions.Editable;

namespace Simple.Entities
{
    internal static class ExpressionExtensions
    {
        public static T Funcletize<T>(this T expr)
            where T : Expression
        {
            return (T)Funcletizer.PartialEval(expr);
        }

        public static IList<LazyExpression<Func<T, object>>> ToEditable<T>(this IList<Expression<Func<T, object>>> expressions)
        {
            return expressions.Select(x => x.ToLazyExpression()).ToList();
        }
    }

}
