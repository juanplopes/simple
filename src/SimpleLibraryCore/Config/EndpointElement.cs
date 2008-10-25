using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class EndpointElement : ConfigElement
    {
        [ConfigElement("bindingNameRef", Required=true)]
        public string BindingNameRef { get; set; }

        [ConfigElement("bindingType",Required=true)]
        public TypeConfigElement BindingType { get; set; }

        [ConfigElement("address",Default="")]
        public string Address { get; set; }

        [ConfigElement("endpointConfiguratorType")]
        public List<ConfiguratorElement> EndpointConfigurators { get; set; }
    }
}
