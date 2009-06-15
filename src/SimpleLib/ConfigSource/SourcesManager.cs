using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;

namespace Simple.ConfigSource
{
    public class DefaultSource
    {
        public static DefaultSource Instance = new DefaultSource();
        private DefaultSource() { }
    }
    public static class SourcesManager<C>
    {
        private static IDictionary<object, IConfigSource<C>> sources = new Dictionary<object, IConfigSource<C>>();
        private static IDictionary<object, Factory<C>> factories = new Dictionary<object, Factory<C>>();

        public static void RegisterSource(object key, IConfigSource<C> source)
        {
            lock (sources)
                sources[key] = source;
        }

        public static void RegisterSource(IConfigSource<C> source)
        {
            RegisterSource(DefaultSource.Instance, source);
        }

        private static IConfigSource<C> GetSource(object key)
        {
            try
            {
                return sources[key];
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException("This factory has no source for the key " + key.ToString());
            }
        }

        public static bool HasSource(object key)
        {
            return sources.ContainsKey(key);
        }

        public static void RemoveSource(object key)
        {
            lock (sources)
                if (HasSource(key))
                    sources.Remove(key);
        }

        public static void RemoveSource()
        {
            RemoveSource(DefaultSource.Instance);
        }

        public static bool HasSource()
        {
            return HasSource(DefaultSource.Instance);
        }

        public static F GetFactory<F>(object key)
            where F : Factory<C>, new()
        {
            lock (factories)
            {
                Factory<C> res = null;

                if (!factories.TryGetValue(key, out res))
                {
                    res = new F();
                    res.Init(GetSource(key));
                    factories[key] = res;
                }

                return (F)res;
            }
        }

        public static F GetFactory<F>()
            where F : Factory<C>, new()
        {
            return GetFactory<F>(DefaultSource.Instance);
        }

    }
}
