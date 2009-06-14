using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public static class ConfigSources
    {
        public static readonly object Default = new object();

        private static IDictionary<object, ConfigRepository> Repositories { get; set; }
        static ConfigSources()
        {
            Repositories = new Dictionary<object, ConfigRepository>();
        }

        public static ConfigRepository Get(object key)
        {
            lock (Repositories)
            {
                ConfigRepository res = null;
                if (!Repositories.TryGetValue(key, out res))
                {
                    res = new ConfigRepository();
                    Repositories[key] = res;
                }
                return res;
            }
        }

        public static ConfigRepository Get()
        {
            return Get(Default);
        }

        public static T GetDefault<T>()
        {
            return Get().Get<T>();
        }

        public static void SetDefaultSource<T>(IConfigSource<T> source)
        {
            Get().SetSource<T>(source);
        }
    }
}
