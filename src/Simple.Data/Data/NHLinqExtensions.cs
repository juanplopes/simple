using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple
{
    public static class NHLinqExtensions
    {
        public static TResult SafeAggregate<TSource, TResult>(this IQueryable<TSource> source, Func<IQueryable<TSource>, TResult> selector)
        {
            return source.SafeAggregate(selector, default(TResult));
        }

        public static TResult SafeAggregate<TSource, TResult>(this IQueryable<TSource> source, Func<IQueryable<TSource>, TResult> selector, TResult defaultValue)
        {
            if (source.Count() > 0)
                return selector(source);
            else
                return defaultValue;
        }
    }
}
