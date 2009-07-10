using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class EndpointElement : ConfigElement
    {
        [ConfigElement("address",Default="")]
        public string Address { get; set; }

        [ConfigElement("endpointConfiguratorType")]
        public List<ConfiguratorElement> EndpointConfigurators { get; set; }
    }
}
