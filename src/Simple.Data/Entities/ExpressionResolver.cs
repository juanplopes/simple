using System;
using System.Linq;
using System.Linq.Expressions;
using Simple.Entities.QuerySpec;

namespace Simple.Entities
{
    public class ExpressionResolver<T> : IExpressionResolver<T>
    {
        public IQueryable<T> Expr(IQueryable<T> query, Expression<Func<IQueryable<T>, IQueryable<T>>> expr)
        {
            return expr.Compile()(query);
        }
    }
}
