using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using BasicLibrary.Configuration;
using System.ServiceModel.Channels;
using BasicLibrary.Common;

namespace SimpleLibrary.ServiceModel
{
    public class WsdlSpecialExporter : WsdlExporter
    {
        public IList<string> RemoveMetaSection { get; set; }

        public WsdlSpecialExporter(IEnumerable<string> toRemove)
        {
            RemoveMetaSection = new List<string>(toRemove);
        }

        public override MetadataSet GetGeneratedMetadata()
        {
            MetadataSet set = base.GetGeneratedMetadata();

            for(int i=set.MetadataSections.Count-1; i>=0; i--)
            {
                foreach(string s in RemoveMetaSection)
                    if (new Uri(set.MetadataSections[i].Identifier) == new Uri(s))
                    {
                        set.MetadataSections.RemoveAt(i);
                        break;
                    }
            }

            return set;
        }
    }

    public class MetadataBehaviorServiceConfigurator : IServiceConfigurator
    {
        public void Configure(System.ServiceModel.ServiceHost service, SimpleLibrary.Config.ConfiguratorElement config)
        {
            MetadataBehaviorServiceConfiguratorConfig configT = new MetadataBehaviorServiceConfiguratorConfig();
            (configT as IConfigElement).LoadFromElement(config);
            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = configT.HttpGetEnabled;
            behavior.MetadataExporter = new WsdlSpecialExporter(configT.RemoveMetaSection);

            service.Description.Behaviors.Remove<ServiceMetadataBehavior>();
            service.Description.Behaviors.Add(behavior);
            
            service.AddServiceEndpoint(typeof(IMetadataExchange), 
                new CustomBinding(new HttpTransportBindingElement()), 
                configT.Address);
        }
    }

    public class MetadataBehaviorServiceConfiguratorConfig : ConfigElement
    {
        [ConfigElement("address")]
        public string Address { get; set; }

        [ConfigElement("httpGetEnabled")]
        public bool HttpGetEnabled { get; set; }

        [ConfigElement("removeMetaSection")]
        public List<string> RemoveMetaSection { get; set; }
    }
}
