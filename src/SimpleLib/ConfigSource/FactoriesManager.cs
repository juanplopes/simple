using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.ConfigSource
{
    public class FactoriesManager<F, C> : Dictionary<object, F>
        where F : class, IFactory<C>
        where C : new()
    {
        protected Func<F> HowToBuild { get; set; }

        public FactoriesManager() : base() { }

        public FactoriesManager(Func<F> howToBuild)
        {
            this.HowToBuild = howToBuild;
        }

        public FactoriesManager(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public F SafeGetEx(object key, Func<F> howToBuild)
        {
            lock (this)
            {
                howToBuild = howToBuild ?? HowToBuild;

                F factory = null;
                if (!this.TryGetValue(key, out factory))
                {
                    if (howToBuild == null) throw new InvalidOperationException("I don't know how to build factory!");
                    
                    factory = howToBuild();
                    SourcesManager.Configure(key, factory);
                    this[key] = factory;
                }

                return factory;
            }
        }

        public F SafeGetEx(Func<F> howToBuild)
        {
            return SafeGetEx(SourcesManager.DefaultKey, howToBuild);
        }

        public F SafeGet()
        {
            return SafeGetEx(null);
        }

        public F SafeGet(object key)
        {
            return SafeGetEx(key, null);
        }
    }
}
