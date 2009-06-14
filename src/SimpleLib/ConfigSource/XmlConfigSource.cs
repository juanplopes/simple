using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Simple.Persistence;
using System.IO;
using System.Xml;
using Simple.IO;

namespace Simple.ConfigSource
{
    public class XmlConfigSource<T> : IConfigSource<T>
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(T));
        public Stream Source { get; set; }

        public XmlConfigSource(Stream stream)
        {
            Source = stream;
        }

        public XmlConfigSource(string xml)
        {
            Source = XmlHelper.GetStream(xml);
        }

        public XmlConfigSource(XmlNode node)
        {
            Source = XmlHelper.GetStream(node);
        }

        public T Load()
        {
            return (T)serializer.Deserialize(Source);
        }

        public event HandleConfigExpired<T> Expired;
    }
}
