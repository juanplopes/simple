using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace BasicLibrary.Common
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
    public class Triplet<T, Q, P> : Pair<T, Q>
    {
        public P Third { get; set; }

        public Triplet(T first, Q second, P third)
            : base(first, second)
        {
            Third = third;
        }
    }
}
