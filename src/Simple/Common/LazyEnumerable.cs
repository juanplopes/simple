using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO.Serialization;
using System.Runtime.Serialization;

namespace Simple.Common
{
    [Serializable]
    public class LazyEnumerable<T> : LazySerializer<IEnumerable<T>, T[]>, IEnumerable<T>
    {
        public LazyEnumerable(IEnumerable<T> real) : base(real) { }
        protected LazyEnumerable(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override IEnumerable<T> TransformToReal(T[] proxy)
        {
            return proxy;
        }

        protected override T[] TransformToProxy(IEnumerable<T> real)
        {
            return real.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Real.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Real.GetEnumerator();
        }
    }
}
