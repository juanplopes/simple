using System.Linq;
using Simple.Entities.QuerySpec;
namespace Simple.Entities
{
    public class LimitsResolver<T> : ILimitsResolver<T>
    {
        #region ILimitsResolver<T> Members

        public IQueryable<T> Skip(IQueryable<T> query, int skip)
        {
            return query.Skip(skip);
        }

        public IQueryable<T> Take(IQueryable<T> query, int take)
        {
            return query.Take(take);
        }

        #endregion
    }
}
