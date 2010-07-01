using System;
using Simple.Config;

namespace Simple
{
    public static class SimplyFluentExtensions
    {
        public static T FluentlyDo<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }
       
    }

    public class Simply : AggregateFactory<Simply>
    {
        public void Nop() { }
        public Version GetVersion()
        {
            return this.GetType().Assembly.GetName().Version;
        }

        public SimplyConfigure Configure
        {
            get { return SimplyConfigure.Do[ConfigKey]; }
        }

        public SimplyRelease Release
        {
            get { return SimplyRelease.Do[ConfigKey]; }
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
            return new ConfiguratorInterface<T, SimplyConfigure>(The<T>);
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
