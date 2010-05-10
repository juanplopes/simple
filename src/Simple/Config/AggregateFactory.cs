using System;
using System.Collections.Generic;

namespace Simple.Config
{
    /// <summary>
    /// Base class for config based factories.
    /// </summary>
    /// <typeparam name="THIS">The class itself (for static inheritance purposes).</typeparam>
    public class AggregateFactory<THIS> : MarshalByRefObject
        where THIS : AggregateFactory<THIS>, new()
    {
        protected AggregateFactory()
        {
        }

        /// <summary>
        /// The initialized configuration key object.
        /// </summary>
        public object ConfigKey { get; private set; }


        protected object BestKeyOf(params object[] keys)
        {
            return SourceManager.Do.BestKeyOf(keys);
        }

        static Dictionary<object, THIS> _instances = new Dictionary<object, THIS>();
        
        /// <summary>
        /// Singleton instance accessor.
        /// </summary>
        public static THIS Do
        {
            get
            {
                return Get(null);
            }
        }

        /// <summary>
        /// Projects the factory into another configuration key.
        /// </summary>
        public THIS this[object key]
        {
            get { return Get(key); }
        }

        /// <summary>
        /// Gets an specific keyed configuration.
        /// </summary>
        /// <param name="key">The configuration key object.</param>
        /// <returns>An instance of the factory.</returns>
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
