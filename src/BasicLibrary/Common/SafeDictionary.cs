using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.Common
{
    [Serializable]
    public class SafeDictionary<K, V> : Dictionary<K, V>
    {
        public SafeDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public SafeDictionary() : base() { }
        public SafeDictionary(IEqualityComparer<K> comparer) : base(comparer) { }

        public new V this[K key]
        {
            get
            {
                try
                {
                    return base[key];
                }
                catch (KeyNotFoundException)
                {
                    return default(V);
                }
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
