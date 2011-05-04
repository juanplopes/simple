using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple
{
    public static partial class AggregateOrDefaultExtensions
    {
        private static TSource SomethingOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue, int seed)
           where TSource : IComparable
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            TSource max = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current != null && seed * current.CompareTo(max) > 0)
                    max = current;
            }
            return max;
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
           where TSource : IComparable
        {
            return source.MaxOrDefault(default(TSource));
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
            where TSource : IComparable
        {
            return source.SomethingOrDefault(defaultValue, 1);
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable
        {
            return source.MaxOrDefault(default(TSource));
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource?> source, TSource defaultValue)
            where TSource : struct, IComparable
        {
            return source.Where(x => x != null).Select(x => x ?? default(TSource)).MaxOrDefault(defaultValue);
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
          where TSource : IComparable
        {
            return source.MinOrDefault(default(TSource));
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
            where TSource : IComparable
        {
            return source.SomethingOrDefault(defaultValue, -1);
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable
        {
            return source.MinOrDefault(default(TSource));
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource?> source, TSource defaultValue)
            where TSource : struct, IComparable
        {
            return source.Where(x => x != null).Select(x => x ?? default(TSource)).MinOrDefault(defaultValue);
        }
    }
}
