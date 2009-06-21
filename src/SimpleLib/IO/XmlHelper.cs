using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Simple.ConfigSource;

namespace Simple.IO
{
    public class XmlHelper
    {
        public static string QuickSerialize(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, obj);
            StreamReader reader = new StreamReader(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return reader.ReadToEnd();
        }

        public static Stream GetStream(string xml)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(xml);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public static Stream GetStream(XmlNode node)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(stream);
            node.WriteTo(writer);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
