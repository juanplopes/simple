using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;
using Simple.Entities.QuerySpec;

namespace Simple.Entities
{
    public class FetchResolver<T> : IFetchResolver<T>
    {
        #region IFetchResolver<T> Members

        public IQueryable<T> Fetch<P>(IQueryable<T> query, Expression<Func<T, P>> expr)
        {
            return query.Fetch(expr);
        }

        public IQueryable<T> FetchMany<P>(IQueryable<T> query, Expression<Func<T, IEnumerable<P>>> expr)
        {
            return query.FetchMany(expr);
        }

        public IQueryable<T> ThenFetch<P, Q>(IQueryable<T> query, Expression<Func<P, Q>> expr)
        {
            var right = (NhFetchRequest<T, P>)query;
            return right.ThenFetch(expr);
        }

        public IQueryable<T> ThenFetchMany<P, Q>(IQueryable<T> query, Expression<Func<P, IEnumerable<Q>>> expr)
        {
            var right = (NhFetchRequest<T, P>)query;
            return right.ThenFetchMany(expr);
        }

        #endregion
    }
}
