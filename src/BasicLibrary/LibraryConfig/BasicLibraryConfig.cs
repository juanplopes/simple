using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace BasicLibrary.LibraryConfig
{
    [DefaultFile("BasicLibrary.config", ThrowException = false, LoadDefaultFirst = true)]
    public class BasicLibraryConfig : ConfigRoot<BasicLibraryConfig>
    {
        [ConfigElement("log4net", Required = true)]
        public Log4netConfig Log4net { get; set; }

        public override string DefaultXmlString
        {
            get
            {
                return DefaultConfigResource.BasicLibraryConfig;
            }
        }
    }
}
