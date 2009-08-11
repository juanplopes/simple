using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public class AggregateFactory<THIS> : MarshalByRefObject
        where THIS : AggregateFactory<THIS>, new()
    {
        protected AggregateFactory()
        {
        }
        public object ConfigKey { get; private set; }

        static Dictionary<object, THIS> _instances = new Dictionary<object, THIS>();
        public static THIS Do
        {
            get
            {
                return Get(null);
            }
        }

        public THIS this[object key]
        {
            get { return Get(key); }
        }

        public static THIS Get(object key)
        {
            lock (_instances)
            {
                if (key == null) key = SourceManager.Do.DefaultKey;

                THIS ret;
                if (!_instances.TryGetValue(key, out ret))
                {
                    ret = new THIS();
                    ret.ConfigKey = key;
                    _instances[key] = ret;
                }
                return ret;
            }
        }
    }
}
