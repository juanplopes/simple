using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Xml;

namespace Simple.Cfg
{
    public class NHibernateConfig : IXmlContentHolder
    {
        public XmlElement Element { get; set; }
    }
}
