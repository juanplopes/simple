using System.Xml;
using Simple.Config;

namespace Simple.Data
{
    public class NHibernateConfig : IXmlContentHolder
    {
        public XmlElement Element { get; set; }
    }
}
