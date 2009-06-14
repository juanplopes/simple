using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class BaseConfigSource<T> : IConfigSource<T>
    {
        public bool Loaded
        {
            get
            {
                return !Cache.Equals(default(T));
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
