using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Simple.Config
{
    public class LoggerConfig
    {
        [XmlElement("log4net")]
        public XmlElement Log4net { get; set; }
    }
}
