using System;
using log4net;
using Simple.Logging;
using System.Reflection;
using Simple.ConfigSource;

namespace Simple
{
    public class Simply : AggregateFactory<Simply>
    {
        public void Nop() { }

        public SimplyConfigure Configure
        {
            get
            {
                return SimplyConfigure.Do[ConfigKey];
            }
        }

        public SimplyRelease Release
        {
            get
            {
                return SimplyRelease.Do[ConfigKey];
            }
        }

        public T GetConfig<T>()
        {
            return SourceManager.Do.Get<T>(this.ConfigKey).Get();
        }
    }

    public class SimplyConfigure : AggregateFactory<SimplyConfigure>
    {
        public IConfiguratorInterface<T, SimplyConfigure> The<T>()
        {
            return new ConfiguratorInterface<T, SimplyConfigure>(x => The(x));
        }

        public SimplyConfigure The<T>(IConfigSource<T> source)
        {
            SourceManager.Do.Register(this.ConfigKey, source);
            return this;
        }

        public SimplyConfigure Factory<T>(IFactory<T> factory)
        {
            SourceManager.Do.AttachFactory(this.ConfigKey, factory);
            return this;

        }
    }

    public class SimplyRelease : AggregateFactory<SimplyRelease>
    {
        public SimplyRelease The<T>()
        {
            SourceManager.Do.Remove<T>(this.ConfigKey);
            return this;
        }
    }
}
