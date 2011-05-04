using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

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

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
             where TSource : IComparable
        {
            return source.SomethingOrDefault(defaultValue, 1);
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
            where TSource : IComparable
        {
            return source.SomethingOrDefault(defaultValue, -1);
        }
    }
}
