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
            var i = enum1.GetEnumerator();
            var j = enum2.GetEnumerator();
            while(i.MoveNext() && j.MoveNext())
            {
                yield return func(i.Current, j.Current);
            }
        }

        public static T AggregateJoin<T>(this IEnumerable<T> enumerable, Func<T, T, T> joiner)
        {
            var result = enumerable.First();

            foreach (var other in enumerable.Skip(1))
                result = joiner(result, other);

            return result;
        }

        public static string StringJoin(this IEnumerable enumerable)
        {
            return enumerable.StringJoin(string.Empty);
        }

        public static string StringJoin(this IEnumerable enumerable, string separator)
        {
            var enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            var builder = new StringBuilder();

            builder.Append(enumerator.Current);
            
            while(enumerator.MoveNext())
                builder.Append(separator).Append(enumerator.Current);

            return builder.ToString();
        }
    }
}
