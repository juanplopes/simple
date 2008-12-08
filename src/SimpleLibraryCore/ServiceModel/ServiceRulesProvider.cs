using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.Rules;
using System.ServiceModel;
using System.ServiceModel.Channels;
using SimpleLibrary.Config;
using System.Net;

namespace SimpleLibrary.ServiceModel
{
    public class ServiceRulesProvider<T> : IRulesProvider<T>
    {
        protected ChannelFactory<T> FactoryCache { get; set; }
        protected SimpleLibraryConfig Config { get; set; }
        protected Binding DefaultBinding { get; set; }

        public ServiceRulesProvider()
        {
            Config = SimpleLibraryConfig.Get();
            DefaultBinding = ConfigLoader.CreateDefaultBinding();
        }

        public T Create(Uri endpointAddress)
        {
            ChannelFactory<T> factory = CreateChannelFactory();
            return factory.CreateChannel(new EndpointAddress(endpointAddress));
        }

        public T Create()
        {
            Type contractType = typeof(T);
            return Create(new Uri(new Uri(Config.ServiceModel.DefaultBaseAddress), contractType.Name));
        }

        protected ChannelFactory<T> CreateChannelFactory()
        {
            if (FactoryCache == null)
            {
                FactoryCache = new ChannelFactory<T>(DefaultBinding);
                ConfigLoader.ApplyConfigurators(FactoryCache.Endpoint, Config.ServiceModel.DefaultEndpoint, true);
            }

            return FactoryCache;
        }
    }
}
