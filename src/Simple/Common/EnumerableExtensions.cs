using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace Simple
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T3> Zip<T1, T2, T3>(this IEnumerable<T1> enum1, IEnumerable<T2> enum2, Func<T1, T2, T3> func)
        {
            using (var i = enum1.GetEnumerator())
            using (var j = enum2.GetEnumerator())
                while (i.MoveNext() && j.MoveNext())
                {
                    yield return func(i.Current, j.Current);
                }
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            try
            {
                return dictionary[key];
            }
            catch (KeyNotFoundException)
            {
                return default(TValue);
            }
        }

        public static T AggregateJoin<T>(this IEnumerable<T> enumerable, Func<T, T, T> joiner)
        {
            var result = enumerable.First();

            foreach (var other in enumerable.Skip(1))
                result = joiner(result, other);

            return result;
        }

        public static IEnumerable<IList<T>> BatchAggregate<T>(this IEnumerable<T> source, int batchSize)
        {
            var list = new List<T>(batchSize);

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                    if (list.Count == batchSize)
                    {
                        yield return list;
                        list = new List<T>(batchSize);
                    }
                }

                if (list.Any())
                    yield return list;
            }
        }

        public static IEnumerable<Q> BatchSelect<T, Q>(this IEnumerable<T> source, int batchSize, Func<IEnumerable<T>, IEnumerable<Q>> func)
        {
            return source.BatchAggregate(batchSize).SelectMany(x => func(x));
        }

        public static IEnumerable<T> EagerForeach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            return enumerable.EagerForeach(action, null);
        }
        public static IEnumerable<T> EagerForeach<T>(this IEnumerable<T> enumerable, Action<T> action, Action<T> between)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return enumerable;

                action(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    if (between != null)
                        between(enumerator.Current);
                    if (action != null)
                        action(enumerator.Current);
                }

                return enumerable;
            }
        }

        public static string StringJoin<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.StringJoin(string.Empty);
        }

        public static string StringJoin<T>(this IEnumerable<T> enumerable, string separator)
        {
            var builder = new StringBuilder();
            enumerable.EagerForeach(x => builder.AppendFormat("{0}", x), x => builder.Append(separator));
            return builder.ToString();
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> enumerable, params T[] items)
        {
            return enumerable.Union((IEnumerable<T>)items);
        }

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

        public static TResult SafeAggregate<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, TResult> selector)
        {
            return source.SafeAggregate(selector, default(TResult));
        }

        public static TResult SafeAggregate<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, TResult> selector, TResult defaultValue)
        {
            if (source.Count() > 0)
                return selector(source);
            else
                return defaultValue;
        }


    }
}
