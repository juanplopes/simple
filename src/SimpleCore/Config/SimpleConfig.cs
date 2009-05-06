using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration;

namespace Simple.Config
{
    [DefaultFile("Simple.config", ThrowException = false, LoadDefaultFirst = true)]
    public class SimpleConfig : ConfigRoot<SimpleConfig>
    {
        [ConfigElement("business", Required = true)]
        public BusinessElement Business { get; set; }

        [ConfigElement("serviceModel", Required = true)]
        public ServiceModelElement ServiceModel { get; set; }

        [ConfigElement("dataConfig", Required = true)]
        public DataConfigElement DataConfig { get; set; }

        [ConfigElement("threading", Default = InstanceType.New)]
        public ThreadingElement Threading { get; set; }

        public override string DefaultXmlString
        {
            get
            {
                return DefaultConfigContent.SimpleConfig;
            }
        }
    }
}
