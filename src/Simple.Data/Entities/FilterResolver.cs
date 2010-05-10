using System;
using System.Linq;
using System.Linq.Expressions;
using Simple.Entities.QuerySpec;

namespace Simple.Entities
{
    public class FilterResolver<T> : IFilterResolver<T>
    {
        #region IFilterResolver<T> Members

        public IQueryable<T> Filter(IQueryable<T> query, Expression<Func<T, bool>> expr)
        {
            return query.Where(expr);
        }

        #endregion
    }
}
