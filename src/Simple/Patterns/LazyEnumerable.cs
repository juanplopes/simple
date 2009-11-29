using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Patterns
{
    [Serializable]
    public class LazyEnumerable<T> : IEnumerable<T>
    {
        private LinkedList<T> _cache;
        private IEnumerator<T> _enumerator;


        private LazyEnumerable()
        {
            _cache = new LinkedList<T>();
        }

        public LazyEnumerable(IEnumerator<T> baseEnumerator) : this()
        {
            _enumerator = baseEnumerator;
        }

        public LazyEnumerable(IEnumerable<T> baseEnumerable) :
            this(baseEnumerable.GetEnumerator()) { }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T t in _cache)
                yield return t;

            while (_enumerator.MoveNext())
            {
                _cache.AddLast(_enumerator.Current);
                yield return _enumerator.Current;
            }
        }

        public IEnumerable<T> Activate()
        {
            foreach(T t in this);

            return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }
}
