using System;
using System.Linq;
using System.Linq.Expressions;
using Simple.Entities.QuerySpec;

namespace Simple.Entities
{
    public class OrderByResolver<T> : IOrderByResolver<T>
    {
        #region IOrderByResolver<T> Members

        public IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expr)
        {
            return query.OrderBy(expr);
        }

        public IQueryable<T> ThenBy(IQueryable<T> query, Expression<Func<T, object>> expr)
        {
            var right = (IOrderedQueryable<T>)query;
            return right.ThenBy(expr);
        }

        public IQueryable<T> OrderByDescending(IQueryable<T> query, Expression<Func<T, object>> expr)
        {
            return query.OrderByDescending(expr);
        }

        public IQueryable<T> ThenByDescending(IQueryable<T> query, Expression<Func<T, object>> expr)
        {
            var right = (IOrderedQueryable<T>)query;
            return right.ThenByDescending(expr);
        }

        #endregion
    }
}
