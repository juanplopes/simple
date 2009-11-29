using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.Patterns
{
    [Serializable]
    public class TransformationList<T> : LinkedList<Func<T,T>>
    {
        public TransformationList(IEnumerable<Func<T,T>> collection) : base(collection) { }
        public TransformationList(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public TransformationList() : base() { }

        public void Add(Func<T, T> func)
        {
            this.AddLast(func);
        }

        public void AddRange(IEnumerable<Func<T,T>> enumerable)
        {
            lock (this)
            {
                foreach (Func<T, T> func in enumerable)
                {
                    Add(func);
                }
            }
        }

        public T StaticInvoke(T obj, IEnumerable<Func<T, T>> transformations)
        {
            T temp = obj;
            foreach (var func in transformations)
            {
                temp = func(temp);
            }
            return temp;
        }

        public T Invoke(T obj)
        {
            lock (this)
            {
                return StaticInvoke(obj, this);
            }
        }

        public static TransformationList<T> operator +(TransformationList<T> list, Func<T, T> func)
        {
            list.Add(func);
            return list;
        }

        public static TransformationList<T> operator -(TransformationList<T> list, Func<T, T> func)
        {
            list.Remove(func);
            return list;
        }
    }
}
