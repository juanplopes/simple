using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Simple.Persistence;
using System.IO;
using System.Xml;
using Simple.IO;
using Simple.Reflection;

namespace Simple.ConfigSource
{
    public class XmlConfigSource<T> :
        ConfigSource<T>,

        IXPathConfigSource<T, Stream>,
        IXPathConfigSource<T, string>,
        IXPathConfigSource<T, XmlElement>,
        IXPathConfigSource<T, XmlDocument>,
        IXPathConfigSource<T, XmlReader>,
        IXPathConfigSource<T, TextReader>
        where T : new()
    {
        protected bool IsXmlContainer
        {
            get { return TypesHelper.CanAssign<T, IXmlContentHolder>(); }
        }

        #region How do we load... Boring...

        public IConfigSource<T> Load(XPathParameter<XmlReader> input)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(input.Parameter);
            return Load(input.ToOther(doc.DocumentElement));

        }
        public IConfigSource<T> Load(XPathParameter<XmlElement> input)
        {
            XmlElement element = input.Parameter.SelectSingleNode(input.XPath) as XmlElement;
            if (element == null) throw new InvalidOperationException("Xml node not found");

            if (IsXmlContainer)
            {
                T t = new T();
                (t as IXmlContentHolder).Element = element;
                Cache = t;
                return this;
            }
            else
            {
                using (Stream stream = new MemoryStream())
                {
                    XmlWriter w = XmlWriter.Create(stream);
                    element.WriteTo(w); w.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    Cache = (T)serializer.Deserialize(stream);
                    return this;
                }
            }
        }
        public IConfigSource<T> Load(XPathParameter<string> input)
        {
            using (Stream stream = XmlHelper.GetStream(input.Parameter))
            {
                return Load(input.ToOther(stream));
            }
        }
        public IConfigSource<T> Load(XPathParameter<Stream> input)
        {
            return Load(input.ToOther(XmlReader.Create(input.Parameter)));
        }
        public IConfigSource<T> Load(XPathParameter<XmlDocument> input)
        {
            return Load(input.ToOther(input.Parameter.DocumentElement));
        }
        public IConfigSource<T> Load(XPathParameter<TextReader> input)
        {
            return Load(input.ToOther(XmlReader.Create(input.Parameter)));
        }
        #endregion


    }
}
