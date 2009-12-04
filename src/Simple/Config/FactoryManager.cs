using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Simple.Patterns;

namespace Simple.Config
{
    /// <summary>
    /// Manages a factory class, attaching it to the correct handlers.
    /// </summary>
    /// <typeparam name="F">The factory type.</typeparam>
    /// <typeparam name="C">The config type.</typeparam>
    public class FactoryManager<F, C>
        where F : class, IFactory<C>
    {
        protected Func<F> HowToBuild { get; set; }
        protected Dictionary<object, F> Factories { get; set; }

        /// <summary>
        /// Initializes with the default <typeparamref name="F"/> constructor.
        /// </summary>
        public FactoryManager() : this(() => Activator.CreateInstance<F>()) { }

        /// <summary>
        /// Initializes defining a way to construct <typeparamref name="F"/>.
        /// </summary>
        /// <param name="howToBuild">A function defining how to build the factory.</param>
        public FactoryManager(Func<F> howToBuild)
        {
            this.HowToBuild = howToBuild;
            this.Factories = new Dictionary<object, F>();
        }

        /// <summary>
        /// Tries to get a factory attaching it to <see cref="SourceManager"/>.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <param name="howToBuild">A function defining how to build the factory.</param>
        /// <returns>The factory.</returns>
        public F SafeGetEx(object key, Func<F> howToBuild)
        {
            lock (Factories)
            {
                if (key == null) key = SourceManager.Do.DefaultKey;

                howToBuild = howToBuild ?? HowToBuild;

                F factory = null;
                if (!Factories.TryGetValue(key, out factory))
                {
                    if (howToBuild == null) throw new InvalidOperationException("I don't know how to build factory!");

                    factory = howToBuild();
                    SourceManager.Do.AttachFactory(key, factory);
                    Factories[key] = factory;
                }

                return factory;
            }
        }

        /// <summary>
        /// Constructs the factory with default config telling it how to build.
        /// </summary>
        /// <param name="howToBuild">The function defining how to build.</param>
        /// <returns>The factory.</returns>
        public F SafeGetEx(Func<F> howToBuild)
        {
            return SafeGetEx(null, howToBuild);
        }

        /// <summary>
        /// Constructs the factory with default config and default constructor.
        /// </summary>
        /// <returns></returns>
        public F SafeGet()
        {
            return SafeGetEx(null);
        }

        /// <summary>
        /// Constructs the factory with alternative config, but default constructor.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public F SafeGet(object key)
        {
            return SafeGetEx(key, null);
        }

        public F this[object key]
        {
            get
            {
                return SafeGet(key);
            }
        }
    }
}
