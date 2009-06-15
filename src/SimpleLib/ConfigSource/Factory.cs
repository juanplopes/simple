using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class Factory<T>
    {
        public bool Initialized { get; set; }

        public void CheckInitialized()
        {
            if (!Initialized) throw new InvalidOperationException("Factory not initialized");
        }

        public void Init(IConfigSource<T> source)
        {
            lock (this)
            {
                T config = source.Get();
                InitializeObjects(config);
                source.Reloaded += InitializeObjects;
                Initialized = true;
            }
        }

        protected abstract void InitializeObjects(T config);

    }
}
