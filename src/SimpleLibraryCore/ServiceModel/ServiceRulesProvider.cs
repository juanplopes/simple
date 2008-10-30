using System;
using System.Collections.Generic;
using System.Linq;
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

        public ServiceRulesProvider()
        {
            Config = SimpleLibraryConfig.Get();
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
                Binding binding = ConfigLoader.CreateBinding(Config.ServiceModel.DefaultEndpoint);
                FactoryCache = new ChannelFactory<T>(binding);
                ConfigLoader.ApplyConfigurators(FactoryCache.Endpoint, Config.ServiceModel.DefaultEndpoint, true);
            }

            return FactoryCache;
        }
    }
}
