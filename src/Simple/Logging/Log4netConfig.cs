using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Simple.Config;


namespace Simple.Logging
{
    public class Log4netConfig : IXmlContentHolder
    {
        public XmlElement Element { get; set; }
    }
}
