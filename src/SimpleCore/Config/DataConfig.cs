using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Simple.Config
{
    public class DataConfig
    {
        [XmlElement("nhibernate-configuration-2.2")]
        public XmlElement NHibernateConfiguration { get; set; }

        [XmlElement("file")]
        public string FileName { get; set; }
    }
}
