using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
 
    public class ConfigRepository
    {
        protected IDictionary<Type, object> SingleInstanceConfig { get; set; }
        protected IDictionary<Type, object> SingleInstanceSources { get; set; }

        public ConfigRepository()
        {
            SingleInstanceConfig = new Dictionary<Type, object>();
            SingleInstanceSources = new Dictionary<Type, object>();
        }

        protected void Set<T>(T config)
        {
            lock(SingleInstanceConfig) 
                SingleInstanceConfig[typeof(T)] = config;
        }

        public void SetSource<T>(IConfigSource<T> source)
        {
            lock (SingleInstanceConfig)
            {
                source.Reloaded += Set<T>;
                SingleInstanceSources[typeof(T)] = source; 
                Set(source.Get());
            }
        }

        public T Get<T>()
        {
            lock (SingleInstanceConfig)
            {
                try
                {
                    return (T)SingleInstanceConfig[typeof(T)];
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException("Type not found " + typeof(T).Name);
                }
            }
        }

        public IConfigSource<T> GetSource<T>()
        {
            lock (SingleInstanceConfig)
            {
                try
                {
                    return (IConfigSource<T>)SingleInstanceSources[typeof(T)];
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException("Type not found " + typeof(T).Name);
                }
            }
        }

        public void AddExpiredHandler<T>(ConfigReloadedDelegate<T> del)
        {
            GetSource<T>().Reloaded += del;
        }
    }
}
