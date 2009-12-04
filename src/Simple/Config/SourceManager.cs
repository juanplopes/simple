using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;
using Simple.Patterns;

namespace Simple.Config
{
    /// <summary>
    /// Manages the configs and theirs sources, for specific configuration key objects.
    /// </summary>
    public class SourceManager : Singleton<SourceManager>
    {
        /// <summary>
        /// Default object key. Responds to <code>null</code> instances.
        /// </summary>
        public object DefaultKey = new object();

        /// <summary>
        /// Gets <see cref="IConfigSource<C>" /> instance associated to <typeparamref name="C"/> type.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <returns>An instance of the configuration source.</returns>
        public IConfigSource<C> Get<C>()
        {
            return Get<C>(null);
        }

        /// <summary>
        /// Gets <see cref="IConfigSource<C>" /> instance associated to <typeparamref name="C"/> type and key object.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <param name="key">The config key.</param>
        /// <returns>An instance of the configuration source.</returns>
        public IConfigSource<C> Get<C>(object key)
        {
            return SourceFor<C>.Do.Get(key);
        }

        /// <summary>
        /// Registers a configuration source for some config type.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <param name="source">The config source.</param>
        public void Register<C>(IConfigSource<C> source)
        {
            Register(null, source);
        }

        /// <summary>
        /// Registers a configuration source for some config type and config key.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <param name="key">The config key</param>
        /// <param name="source">The config source.</param>
        public void Register<C>(object key, IConfigSource<C> source)
        {
            SourceFor<C>.Do.Set(key, source);
        }

        /// <summary>
        /// Remove the config source associated to <typeparamref name="C"/>.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        public void Remove<C>()
        {
            Remove<C>(null);
        }

        /// <summary>
        /// Remove the config source associated to <typeparamref name="C"/> in the specified config key.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <param name="key">The config key.</param>
        public void Remove<C>(object key)
        {
            SourceFor<C>.Do.Set(key, NullConfigSource<C>.Instance);
        }

        /// <summary>
        /// Clears all config sources in all config key for an specified config type.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        public void Clear<C>()
        {
            SourceFor<C>.Do.Clear();
        }

        /// <summary>
        /// Attach a factory instance to a config type and its source, to init config communications.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <param name="factory">The factory.</param>
        public void AttachFactory<C>(IFactory<C> factory)
        {
            AttachFactory(null, factory);
        }

        /// <summary>
        /// Attach a factory instance to a config type and its source for some config key, to init config communications.
        /// </summary>
        /// <typeparam name="C">The config type.</typeparam>
        /// <param name="key">The config key.</param>
        /// <param name="factory">The factory.</param>
        public void AttachFactory<C>(object key, IFactory<C> factory)
        {
            factory.Init(SourceFor<C>.Do.Get(key));
        }

        /// <summary>
        /// Selects in a list of keys, the first that matches the needs.
        /// </summary>
        /// <param name="keys">The keys collection.</param>
        /// <returns>The first correct key.</returns>
        public object BestKeyOf(params object[] keys)
        {
            object ret = null;
            foreach (object obj in keys)
            {
                ret = obj;
                if (ret != null && ret != SourceManager.Do.DefaultKey) break;
            }
            return ret;
        }

        internal class SourceFor<C> : Singleton<SourceFor<C>>
        {
            private IDictionary<object, WrappedConfigSource<C>> Configs =
                new Dictionary<object, WrappedConfigSource<C>>();

            public WrappedConfigSource<C> Get(object key)
            {
                lock (Configs)
                {
                    if (key == null) key = SourceManager.Do.DefaultKey;

                    WrappedConfigSource<C> ret = null;

                    if (!Configs.TryGetValue(key, out ret))
                    {
                        ret = new WrappedConfigSource<C>().Load(NullConfigSource<C>.Instance)
                            as WrappedConfigSource<C>;


                        Configs[key] = ret;
                    }

                    return ret;
                }
            }
            public void Set(object key, IConfigSource<C> source)
            {
                lock (Configs)
                {
                    var wrapped = Get(key);
                    wrapped.Load(source);
                }
            }

            public void Clear()
            {
                lock (Configs)
                {
                    foreach (var item in Configs.Keys)
                    {
                        Set(item, NullConfigSource<C>.Instance);
                    }
                }
            }

        }
    }
}
