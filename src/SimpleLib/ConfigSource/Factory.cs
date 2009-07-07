using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class Factory<T> : IFactory<T>, IDisposable
    {
        public bool Initialized
        {
            get { return Source != null; }
        }

        protected IConfigSource<T> Source { get; set; }
        protected T ConfigCache { get; set; }

        public void CheckInitialized()
        {
            if (!(this as IFactory<T>).Initialized) throw new InvalidOperationException("Factory not initialized");
        }

        public void Init(IConfigSource<T> source)
        {
            lock (this)
            {
                if (Source != null)
                    Source.Reloaded -= Config;

                Source = source;

                ConfigCache = Source.Get();
                Config(ConfigCache);
                source.Reloaded += Config;
            }
        }

        protected void Config(T config)
        {
            lock (this)
            {
                ConfigCache = config;
                if (!object.Equals(default(T), config)) OnConfig(config);
                else OnClearConfig();
            }
        }

        public void Clear()
        {
            Init(new NullConfigSource<T>());
        }

        protected abstract void OnConfig(T config);
        protected abstract void OnClearConfig();

        #region IDisposable Members

        public void Dispose()
        {
            Clear();
        }

        #endregion
    }
}
