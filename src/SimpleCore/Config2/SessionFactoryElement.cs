using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration2;
using System.ServiceModel;

namespace Simple.Config
{
    public class SessionFactoryElement : ConfigElement
    {
        [ConfigElement("configFile", Default = "NHibernate.config")]
        public string ConfigFile { get; set; }
        [ConfigElement("hibernate-configuration", Default = null)]
        public PlainXmlConfigElement NHibernateConfig { get; set; }
        [ConfigElement("name", Default = null)]
        public string Name { get; set; }
    }
}
