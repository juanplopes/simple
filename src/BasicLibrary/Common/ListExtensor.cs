﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BasicLibrary.Common
{
    public static class Enumerable
    {
        public static T GetFirst<T>(IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();

            if (enumerator.MoveNext()) return (T)enumerator.Current;
            return default(T);

        }

        public static IEnumerable<Q> EnumerateCasting<T, Q>(IEnumerable<T> enumerable)
            where T : class, Q
            where Q : class
        {
            return Convert(enumerable, e => e as Q);
        }

        public static void ForEach<T>(IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T t in enumerable)
                action(t);
        }

        public static IEnumerable<T> Filter<T>(IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            foreach (T t in enumerable)
                if (predicate(t))
                    yield return t;
        }

        public static IEnumerable<Q> Convert<T, Q>(IEnumerable<T> enumerable, Converter<T, Q> converter)
        {
            foreach (T t in enumerable)
                yield return converter(t);
        }

        public static IEnumerable<T> EnumerateN<T>(params IEnumerable<T>[] enumerables)
        {
            foreach (IEnumerable<T> enumerable in enumerables)
                foreach (T t in enumerable)
                    yield return t;
        }
    }
}
