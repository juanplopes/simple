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
        IConfigSource<T, Stream>,
        IConfigSource<T, string>,
        IConfigSource<T, XmlNode>,
        IConfigSource<T, XmlDocument>,
        IConfigSource<T, XmlReader>,
        IConfigSource<T, TextReader>
        where T : new()
    {
        #region How do we load... Boring...

        public virtual IConfigSource<T> Load(Stream stream)
        {
            return Load(XmlReader.Create(stream));
        }

        public virtual IConfigSource<T> Load(TextReader input)
        {
            return Load(XmlReader.Create(input));
        }

        public virtual IConfigSource<T> Load(XmlReader input)
        {
            if (!TypesHelper.CanAssign<T, IXmlContentHolder>())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                Cache = (T)serializer.Deserialize(input);
                return this;
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(input);

                T t = new T();
                (t as IXmlContentHolder).Element = doc.DocumentElement;
                Cache = t;
                return this;
            }
        }

        public virtual IConfigSource<T> Load(string input)
        {
            using (var stream = XmlHelper.GetStream(input))
            {
                return Load(stream);
            }
        }

        public virtual IConfigSource<T> Load(XmlNode input)
        {
            using (var stream = XmlHelper.GetStream(input))
            {
                return Load(stream);
            }
        }

        public virtual IConfigSource<T> Load(XmlDocument input)
        {
            return Load(input.DocumentElement);
        }

        #endregion
       
    }
}
