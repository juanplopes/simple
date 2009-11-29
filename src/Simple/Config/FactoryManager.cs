using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Simple.Patterns;

namespace Simple.Config
{
    public class FactoryManager<F, C>
        where F : class, IFactory<C>
    {
        protected Func<F> HowToBuild { get; set; }
        protected Dictionary<object, F> Factories { get; set; }

        public FactoryManager() : this(() => Activator.CreateInstance<F>()) { }
        public FactoryManager(Func<F> howToBuild)
        {
            this.HowToBuild = howToBuild;
            this.Factories = new Dictionary<object, F>();
        }

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
