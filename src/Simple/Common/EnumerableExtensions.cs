using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Common
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
    }
}
