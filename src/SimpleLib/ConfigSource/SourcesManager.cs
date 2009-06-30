using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;

namespace Simple.ConfigSource
{
    internal static class SourceFor<C>
    {
        private static IDictionary<object, WrappedConfigSource<C>> Instances =
            new Dictionary<object, WrappedConfigSource<C>>();

        public static WrappedConfigSource<C> Get(object key)
        {
            lock (Instances)
            {
                if (key == null) key = SourcesManager.DefaultKey;

                WrappedConfigSource<C> ret = null;

                if (!Instances.TryGetValue(key, out ret))
                {
                    ret = new WrappedConfigSource<C>().Load(NullConfigSource<C>.Instance)
                        as WrappedConfigSource<C>;


                    Instances[key] = ret;
                }

                return ret;
            }
        }
        public static void Set(object key, IConfigSource<C> source)
        {
            lock (Instances)
            {
                var wrapped = Get(key);
                wrapped.Load(source);
            }
        }

        public static void Clear()
        {
            lock (Instances)
            {
                Instances.Clear();
            }
        }

    }

    public static class SourcesManager
    {
        public static object DefaultKey = new object();

        public static void RegisterSource<C>(IConfigSource<C> source)
        {
            RegisterSource(null, source);
        }
        public static void RegisterSource<C>(object key, IConfigSource<C> source)
        {
            SourceFor<C>.Set(key, source);
        }

        public static void RemoveSource<C>()
        {
            RemoveSource<C>(null);
        }
        public static void RemoveSource<C>(object key)
        {
            SourceFor<C>.Set(key, NullConfigSource<C>.Instance);
        }

        public static void ClearSources<C>()
        {
            SourceFor<C>.Clear();
        }

        public static void Configure<C>(IFactory<C> factory)
        {
            Configure(null, factory);
        }
        public static void Configure<C>(object key, IFactory<C> factory)
        {
            factory.Init(SourceFor<C>.Get(key));
        }
    }
}
