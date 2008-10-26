using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class SessionFactoryElement : ConfigElement
    {
        [ConfigElement("configFile", Default = "NHibernate.config")]
        public string ConfigFile { get; set; }
        [ConfigElement("name", Default = null)]
        public string Name { get; set; }
    }
}
