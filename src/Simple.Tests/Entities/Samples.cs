using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities.QuerySpec;

namespace Simple.Tests.Entities
{
    public class SampleFilterResolver<T> : IFilterResolver<T>
    {

        #region IFilterResolver<T> Members

        public IQueryable<T> Filter(IQueryable<T> query, System.Linq.Expressions.Expression<Func<T, bool>> expr)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
