
using NHibernate;
using NHibernate.Criterion;
using Simple.Entities;

namespace Simple.Data
{
    public static class Extensions
    {
        public static Page<T> Paginate<T>(ICriteria criteria, int skip, int take)
        {
            ICriteria countCriteria = CriteriaTransformer.Clone(criteria)
                    .SetProjection(Projections.RowCount());
            countCriteria.ClearOrders();

            ICriteria pageCriteria = CriteriaTransformer.Clone(criteria)
                .SetMaxResults(take)
                .SetFirstResult(skip);

            Page<T> pagedResult = new Page<T>(pageCriteria.List<T>(), countCriteria.UniqueResult<int>());

            return pagedResult;
        }
#if false
        public static IQueryable<T> PaginateQueryable<T>(
            IQueryable<T> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }

        public static Page<T> Paginate<T>(
            this IQueryable<T> query, int skip, int take)
        {
            int count = query.Count();
            return new Page<T>(query.PaginateQueryable(skip, take).ToList(), count);
        }
#endif


    }
}
