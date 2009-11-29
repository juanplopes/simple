using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using System.Xml;

namespace Simple.DataAccess
{
    public class NHibernateConfig : IXmlContentHolder
    {
        public XmlElement Element { get; set; }
    }
}
