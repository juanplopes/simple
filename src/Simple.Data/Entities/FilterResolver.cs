using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities.QuerySpec;
using System.Linq.Expressions;

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
