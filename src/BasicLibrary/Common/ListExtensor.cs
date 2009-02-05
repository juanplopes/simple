using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BasicLibrary.Common
{
    public static class ListExtensor
    {
        public static T GetFirst<T>(IList list)
        {
            if (list.Count > 0) return (T)list[0];
            else return default(T);
        }

        public static IEnumerable<Q> EnumerateCasting<T,Q>(IEnumerable<T> enumerable)
            where T : class, Q
        {
            foreach (object obj in enumerable)
                yield return obj as T;
        }

        public static IEnumerable<T> EnumerateN<T>(params IEnumerable<T>[] enumerables)
        {
            foreach (IEnumerable<T> enumerable in enumerables)
                foreach (T t in enumerable)
                    yield return t;
        }
    }
}
