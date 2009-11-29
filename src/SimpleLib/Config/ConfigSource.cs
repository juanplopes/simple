using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;
using Simple.Patterns;

namespace Simple.Config
{
    public delegate T ConfigHookDelegate<T>(T config);

    public abstract class ConfigSource<T> : IConfigSource<T>
    {
        private T _cache = default(T);
        protected T Cache
        {
            get
            {
                return _cache;
            }
            set
            {
                _cache = _transformations.Invoke(value);
            }
        }
        protected TransformationList<T> _transformations = new TransformationList<T>();

        public T Get()
        {
            return Cache;
        }


        public bool Loaded
        {
            get
            {
                return !object.Equals(default(T), Cache);
            }
        }

        public event ConfigReloadedDelegate<T> Reloaded;
        public void InvokeReload()
        {
            if (this.Reload())
            {
                Simply.Do.Log(this)
                    .DebugFormat("Reload was invoked for {0}...", typeof(T));

                if (Reloaded != null)
                    Reloaded.Invoke(this.Get());
            }
        }
        public virtual bool Reload()
        {
            throw new NotSupportedException("This implementation doesn't support reloading");
        }


        public virtual void Dispose()
        {
        }


        #region IConfigSource<T> Members

        public IConfigSource<T> AddTransform(Action<T> func)
        {
            return AddTransform(x =>
            {
                func(x);
                return x;
            });
        }



        public IConfigSource<T> AddTransform(Func<T, T> func)
        {
            if (Loaded)
                Cache = func(Cache);

            _transformations.Add(func);

            return this;
        }

        #endregion
    }
}
