using System;
using System.Linq;
using System.Linq.Expressions;
using Simple.Expressions.Editable;

namespace Simple.Entities.QuerySpec
{
    public interface IExpressionResolver<T>
    {
        IQueryable<T> Expr(IQueryable<T> query, Expression<Func<IQueryable<T>, IQueryable<T>>> expr);
    }

    [Serializable]
    public class ExpressionItem<T> : ISpecItem<T, IExpressionResolver<T>>
    {
        public LazyExpression<Func<IQueryable<T>, IQueryable<T>>> Expression { get; set; }

        public ExpressionItem(Expression<Func<IQueryable<T>, IQueryable<T>>> expr)
        {
            this.Expression = expr.Funcletize().ToLazyExpression();
        }

        public IQueryable<T> Execute(IQueryable<T> query, IExpressionResolver<T> resolver)
        {
            return resolver.Expr(query, Expression.Real);
        }
    }

}
