using System.Xml;
using Simple.Config;


namespace Simple.Logging
{
    public class Log4netConfig : IXmlContentHolder
    {
        public XmlElement Element { get; set; }
    }
}
