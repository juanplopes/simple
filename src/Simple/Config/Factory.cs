using System;

namespace Simple.Config
{
    /// <summary>
    /// Base class for all <see cref="IConfigSource<T>"/> bound factories.
    /// </summary>
    /// <typeparam name="T">The configuration type.</typeparam>
    public abstract class Factory<T> : IFactory<T>, IDisposable
    {
        object _lcok = new object();

        /// <summary>
        /// Indicates whether the factory has a config source associated to it.
        /// </summary>
        public bool Initialized
        {
            get { return Source != null; }
        }

        /// <summary>
        /// The configuration source associated to this factory.
        /// </summary>
        protected IConfigSource<T> Source { get; set; }

        /// <summary>
        /// The last loaded config.
        /// </summary>
        protected T ConfigCache { get; set; }

        /// <summary>
        /// Checks if this factory is initialized, throwing an exception if not.
        /// </summary>
        public void CheckInitialized()
        {
            if (!(this as IFactory<T>).Initialized) throw new InvalidOperationException("Factory not initialized");
        }

        /// <summary>
        /// Initializes the factory using an instance of <see cref="IConfigSource<T>"/>.
        /// </summary>
        /// <param name="source">The source.</param>
        public void Init(IConfigSource<T> source)
        {
            lock (this)
            {
                if (Source != null)
                    Source.Reloaded -= Config;

                Source = source;

                Config(Source.Get());

                source.Reloaded += Config;
            }
        }

        protected virtual void OnDisposeOldConfig() {}

        protected void Config(T config)
        {
            lock (this)
            {
                if (!object.Equals(default(T), ConfigCache)) OnDisposeOldConfig();

                ConfigCache = config;

                if (!object.Equals(default(T), config)) OnConfig(config);
                else OnClearConfig();
            }
        }

        /// <summary>
        /// Clears the configuration, associating an instance of <see cref="NullConfigSource<T>"/>
        /// </summary>
        public void Clear()
        {
            Init(new NullConfigSource<T>());
        }

        protected virtual void OnConfig(T config) { }
        protected virtual void OnClearConfig() { }

        #region IDisposable Members

        public virtual void Dispose()
        {
            Clear();
        }

        #endregion
    }
}
