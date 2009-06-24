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

        public T Invoke(T obj)
        {
            lock (this)
            {
                T temp = obj;
                foreach(var func in this)
                {
                    temp = func(temp);
                }
                return temp;
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
