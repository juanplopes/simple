using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration2;

namespace Simple.Config
{
    public class ServiceModelElement : ConfigElement
    {
        [ConfigElement("defaultEndpoint", Required = true)]
        public EndpointElement DefaultEndpoint { get; set; }

        [ConfigElement("endpoint")]
        [ConfigAcceptsParent("additionalEndpoints")]
        public List<EndpointElement> Endpoints { get; set; }

        [ConfigElement("serviceConfiguratorType")]
        public List<ConfiguratorElement> ServiceConfigurators { get; set; }

        [ConfigElement("defaultBaseAddress", Required = true)]
        public string DefaultBaseAddress { get; set; }
    }
}
