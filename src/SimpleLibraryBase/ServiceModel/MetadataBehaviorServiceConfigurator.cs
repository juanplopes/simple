using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using BasicLibrary.Configuration;

namespace SimpleLibrary.ServiceModel
{
    public class MetadataBehaviorServiceConfigurator : IServiceConfigurator
    {
        public void Configure(System.ServiceModel.ServiceHost service, SimpleLibrary.Config.ConfiguratorElement config)
        {
            MetadataBehaviorServiceConfiguratorConfig configT = new MetadataBehaviorServiceConfiguratorConfig();
            configT.LoadFromElement(config);
            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = configT.HttpGetEnabled;

            service.Description.Behaviors.Remove<ServiceMetadataBehavior>();
            service.Description.Behaviors.Add(behavior);
            
            service.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), configT.Address);
        }
    }

    public class MetadataBehaviorServiceConfiguratorConfig : ConfigElement
    {
        [ConfigElement("address")]
        public string Address { get; set; }

        [ConfigElement("httpGetEnabled")]
        public bool HttpGetEnabled { get; set; }
    }
}
