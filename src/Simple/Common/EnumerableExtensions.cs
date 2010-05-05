using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
