using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class Factory<T> : IFactory<T>
        where T : new()
    {
        bool IFactory<T>.Initialized
        {
            get { return Source != null; }
        }

        protected IConfigSource<T> Source { get; set; }

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

                T config = Source.Get();

                SafeConfig(config);

                source.Reloaded += SafeConfig;
            }
        }

        protected void SafeConfig(T config)
        {
            lock (this)
                if (!object.Equals(default(T), config)) Config(config);
                else ClearConfig();
        }

        protected abstract void Config(T config);
        public abstract void ClearConfig();
    }
}
