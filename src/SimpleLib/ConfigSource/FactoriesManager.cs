using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Simple.Patterns;

namespace Simple.ConfigSource
{
    public class FactoriesManager<F, C>
        where F : class, IFactory<C>
        where C : new()
    {
        protected Func<F> HowToBuild { get; set; }
        protected Dictionary<object, F> Factories { get; set; }

        public FactoriesManager() : this(() => Activator.CreateInstance<F>()) { }
        public FactoriesManager(Func<F> howToBuild)
        {
            this.HowToBuild = howToBuild;
            this.Factories = new Dictionary<object, F>();
        }

        public F SafeGetEx(object key, Func<F> howToBuild)
        {
            lock (Factories)
            {
                if (key == null) key = SourcesManager.DefaultKey;

                howToBuild = howToBuild ?? HowToBuild;

                F factory = null;
                if (!Factories.TryGetValue(key, out factory))
                {
                    if (howToBuild == null) throw new InvalidOperationException("I don't know how to build factory!");

                    factory = howToBuild();
                    SourcesManager.Configure(key, factory);
                    Factories[key] = factory;
                }

                return factory;
            }
        }

        public F SafeGetEx(Func<F> howToBuild)
        {
            return SafeGetEx(null, howToBuild);
        }

        public F SafeGet()
        {
            return SafeGetEx(null);
        }

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
