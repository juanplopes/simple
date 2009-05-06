using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.IO;

namespace Simple.Configuration
{
    public class PlainXmlConfigElement : ConfigElement
    {
        public XmlElement LastXmlElement
        {
            get
            {
                if (this.XmlElements.Count == 0) throw new InvalidConfigurationException("XML content not defined.");
                return this.XmlElements[this.XmlElements.Count - 1];
            }
        }

        public Stream GetStream()
        {
            Stream stream = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(stream);
            LastXmlElement.WriteTo(writer);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
