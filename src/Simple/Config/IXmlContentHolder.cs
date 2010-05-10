using System.Xml;

namespace Simple.Config
{
    /// <summary>
    /// Defines a contract for holding the entire content of a configuration as a XmlElement
    /// </summary>
    public interface IXmlContentHolder
    {
        /// <summary>
        /// The loaded element.
        /// </summary>
        XmlElement Element { get; set; }
    }
}
