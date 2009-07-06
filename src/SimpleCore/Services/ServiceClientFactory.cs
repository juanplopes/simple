using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services
{
    public class ServiceClientFactory : Factory<IServiceClientProvider>
    {
        protected override void OnConfig(IServiceClientProvider config) { }

        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceClientProvider();
        }

        public T Create<T>()
        {
            return (T)ConfigCache.Create(typeof(T));
        }

        public object Create(Type type)
        {
            return ConfigCache.Create(type);
        }
    }
}
