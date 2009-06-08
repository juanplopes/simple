using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Configuration;
using System.ServiceModel;

namespace SimpleLibrary.Config
{
    public class SessionFactoryElement : ConfigElement
    {
        [ConfigElement("configFile", Default = "NHibernate.config")]
        public string ConfigFile { get; set; }
        [ConfigElement("hibernate-configuration", Default = null)]
        public PlainXmlConfigElement NHibernateConfig { get; set; }
        [ConfigElement("executeCommand", Default  = null)]
        public string ExecuteCommand { get; set; }
        [ConfigElement("name", Default = null)]
        public string Name { get; set; }
    }
}
