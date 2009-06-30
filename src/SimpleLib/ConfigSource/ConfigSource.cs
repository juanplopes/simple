using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;

namespace Simple.ConfigSource
{
    public delegate T ConfigHookDelegate<T>(T config);

    public abstract class ConfigSource<T> : IConfigSource<T>
    {
        public bool Loaded
        {
            get
            {
                return !object.Equals(default(T), Cache);
            }
        }
        protected T Cache {get; set;}

        public T Get()
        {
            return Cache;
        }

        public event ConfigReloadedDelegate<T> Reloaded;
        public void InvokeReload() {
            if (this.Reload())
            {
                SimpleLogger.Get(this).DebugFormat("Reload was invoked for {0}...", typeof(T));

                if (Reloaded != null)
                    Reloaded.Invoke(this.Get());
            }
        }

        public virtual void Dispose()
        {
         
        }

        public virtual bool Reload()
        {
            throw new NotSupportedException("This implementation doesn't support reloading");
        }
    }
}
