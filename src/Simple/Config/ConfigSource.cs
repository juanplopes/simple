using System;
using Simple.Patterns;

namespace Simple.Config
{
    /// <summary>
    /// Base configuration source implementation.
    /// </summary>
    /// <typeparam name="T">The configuration type.</typeparam>
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

        /// <summary>
        /// Gets the latest cached value.
        /// </summary>
        /// <returns>The cached value.</returns>
        public T Get()
        {
            return Cache;
        }

        /// <summary>
        /// Indicates whether the configuration source is loaded.
        /// </summary>
        public bool Loaded
        {
            get
            {
                return !object.Equals(default(T), Cache);
            }
        }

        /// <summary>
        /// Occurs when the configuration source receives a source modification event.
        /// </summary>
        public event ConfigReloadedDelegate<T> Reloaded;

        /// <summary>
        /// Forces source reloading, firing the events, when needed.
        /// </summary>
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

        /// <summary>
        /// When overriden in a derived class, reloads the configuration from its source.
        /// </summary>
        /// <returns>True if the configuration could be reloaded. False otherwise.</returns>
        public virtual bool Reload()
        {
            throw new NotSupportedException("This implementation doesn't support reloading");
        }

        public virtual void Dispose()
        {
        }


        #region IConfigSource<T> Members

        /// <summary>
        /// Adds transformation methods to be persistent even when the configuration is reloaded.
        /// </summary>
        /// <param name="func">The function that will be called.</param>
        /// <returns>This instance itself.</returns>
        public IConfigSource<T> AddTransform(Action<T> func)
        {
            return AddTransform(x =>
            {
                func(x);
                return x;
            });
        }

        /// <summary>
        /// Adds transformation methods to be persistent even when the configuration is reloaded.
        /// </summary>
        /// <param name="func">The function that will be called.</param>
        /// <returns>This instance itself.</returns>
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
