using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Expressions;

namespace Simple.Entities
{
    internal static class EntityServiceExtensions
    {
        public static IQueryable<T> ApplyFetch<T>(this IQueryable<T> query, IList<EditableExpression<Func<T, object>>> fetch)
        {
            if (fetch != null)
                foreach (var fetchProperty in fetch)
                {
                    query = query.Fetch(fetchProperty.ToTypedLambda());
                }

            return query;
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, EditableExpression<Func<T, bool>> filter, OrderBy<T> orderBy)
        {
            if (filter != null)
                query = query.Where(filter.ToTypedLambda());

            if (orderBy != null && orderBy.Count > 0)
            {
                IOrderedQueryable<T> tempQuery;
                if (orderBy[0].Backwards)
                    tempQuery = query.OrderByDescending(orderBy[0].ToExpression());
                else
                    tempQuery = query.OrderBy(orderBy[0].ToExpression());

                for (int i = 1; i < orderBy.Count; i++)
                {
                    if (orderBy[i].Backwards)
                        tempQuery = tempQuery.ThenByDescending(orderBy[i].ToExpression());
                    else
                        tempQuery = tempQuery.ThenBy(orderBy[i].ToExpression());

                }
                query = tempQuery;
            }

            return query;
        }

        public static IQueryable<T> ApplyLimit<T>(this IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);
            return query;
        }
    }
}
