using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Simple.Common
{
    [Serializable]
    public class Tuple<T>
    {
        public ReadOnlyCollection<T> Values { get; private set; }

        public int Count { get { return Values.Count; } }

        public Tuple(params T[] values)
        {
            Values = new List<T>(values).AsReadOnly();
        }
    }

    [Serializable]
    public class Pair<T, Q>
    {
        public T First { get; set; }
        public Q Second { get; set; }

        public Pair(T first, Q second)
        {
            First = first;
            Second = second;
        }
    }

    [Serializable]
    public class Pair<T> : Pair<T, T>
    {
        public Pair(T first, T second) : base(first, second) { }
    }

    [Serializable]
    public class Triplet<T, Q, P> : Pair<T, Q>
    {
        public P Third { get; set; }

        public Triplet(T first, Q second, P third)
            : base(first, second)
        {
            Third = third;
        }
    }

    [Serializable]
    public class Triplet<T> : Triplet<T, T, T>
    {
        public Triplet(T first, T second, T third) : base(first, second, third) { }
    }

}
