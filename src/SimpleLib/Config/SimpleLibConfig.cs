using System;
using System.Collections.Generic;
using System.Text;
using Simple.Configuration2;

namespace Simple.Config
{
    [DefaultFile("Simple.config", ThrowException = false, LoadDefaultFirst = true)]
    public class SimpleLibConfig : ConfigRoot<SimpleLibConfig>
    {
        [ConfigElement("log4net", Required = true)]
        public Log4netConfig Log4net { get; set; }

        public override string DefaultXmlString
        {
            get
            {
                return DefaultConfigResource.SimpleConfig;
            }
        }
    }
}
