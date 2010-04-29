using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Expressions;
using Simple.Entities.QuerySpec;
using Simple.Patterns;

namespace Simple.Entities
{
    internal static class EntityServiceExtensions
    {
        public static IQueryable<T> ApplySpecs<T>(this IQueryable<T> query, SpecBuilder<T> specs)
        {
            if (specs == null) return query;
            return query.ApplySpecs(specs.Items);
        }

        public static IQueryable<T> ApplySpecs<T>(this IQueryable<T> query, IList<ISpecItem<T>> specs)
        {
            if (specs == null) return query;

            return specs.Aggregate(query, (q, x) =>
            {
                q = ApplySpec<T, IFilterResolver<T>>(q, x, Singleton<FilterResolver<T>>.Do);
                q = ApplySpec<T, IOrderByResolver<T>>(q, x, Singleton<OrderByResolver<T>>.Do);
                q = ApplySpec<T, IFetchResolver<T>>(q, x, Singleton<FetchResolver<T>>.Do);
                q = ApplySpec<T, ILimitsResolver<T>>(q, x, Singleton<LimitsResolver<T>>.Do);
                return q;
            });
        }

        public static IQueryable<T> ApplySpec<T, R>(this IQueryable<T> query, ISpecItem<T> spec, R resolver)
        {
            var newSpec = spec as ISpecItem<T, R>;
            if (newSpec != null) return newSpec.Execute(query, resolver);
            else return query;
        }
    }
}
