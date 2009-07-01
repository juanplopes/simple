using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services
{
    public class ServiceHostFactory : Factory<IServiceHostProvider>
    {
        protected override void Config(IServiceHostProvider config)
        {
        }

        public override void ClearConfig()
        {
            ConfigCache = new NullServiceHostProvider();
        }

        public void Add(Type type)
        {
            ConfigCache.Add(type);
        }
    }
}
