using System;
using System.Linq;
using System.Linq.Expressions;
using Simple.Expressions;

namespace Simple
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return True<T>(Expression.Parameter(typeof(T), "f"));
        }
        public static Expression<Func<T, bool>> True<T>(ParameterExpression param)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Constant(true), param);
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return False<T>(Expression.Parameter(typeof(T), "f"));
        }
        public static Expression<Func<T, bool>> False<T>(ParameterExpression param)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Constant(false), param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null) expr1 = False<T>();
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters).ExpandInvocations();
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null) expr1 = True<T>();

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters).ExpandInvocations();
        }

        public static Expression<Func<T, bool>> ExpandInvocations<T>(this Expression<Func<T, bool>> expr)
        {
            return InvocationExpander.Expand(expr);
        }
    }
}