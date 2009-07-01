using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class Factory<T> : IFactory<T>
    {
        bool IFactory<T>.Initialized
        {
            get { return Source != null; }
        }

        protected IConfigSource<T> Source { get; set; }
        protected T ConfigCache { get; set; }

        public void CheckInitialized()
        {
            if (!(this as IFactory<T>).Initialized) throw new InvalidOperationException("Factory not initialized");
        }

        void IFactory<T>.Init(IConfigSource<T> source)
        {
            lock (this)
            {
                if (Source != null)
                    Source.Reloaded -= SafeConfig;

                Source = source;

                ConfigCache = Source.Get();

                SafeConfig(ConfigCache);

                source.Reloaded += SafeConfig;
            }
        }

        protected void SafeConfig(T config)
        {
            lock (this)
            {
                ConfigCache = config;
                if (!object.Equals(default(T), config)) Config(config);
                else ClearConfig();
            }
        }

        protected abstract void Config(T config);
        public abstract void ClearConfig();
    }
}
