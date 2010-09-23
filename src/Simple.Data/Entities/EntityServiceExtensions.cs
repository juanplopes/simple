using System.Collections.Generic;
using System.Linq;
using Simple.Entities;
using Simple.Entities.QuerySpec;
using Simple.Patterns;

namespace Simple
{
    public static class EntityServiceExtensions
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
                q = q.ApplySpec<T, IFilterResolver<T>>(x, Singleton<FilterResolver<T>>.Do);
                q = q.ApplySpec<T, IExpressionResolver<T>>(x, Singleton<ExpressionResolver<T>>.Do);
                q = q.ApplySpec<T, IOrderByResolver<T>>(x, Singleton<OrderByResolver<T>>.Do);
                q = q.ApplySpec<T, IFetchResolver<T>>(x, Singleton<FetchResolver<T>>.Do);
                q = q.ApplySpec<T, ILimitsResolver<T>>( x, Singleton<LimitsResolver<T>>.Do);
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
