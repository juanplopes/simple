using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class DataConfigElement : ConfigElement
    {
        [ConfigElement("nhibernateConfigFile", Default="NHibernate.config")]
        public string NHibernateConfigFile { get; set; }
    }
}
