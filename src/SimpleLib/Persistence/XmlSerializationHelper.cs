using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Simple.Persistence
{
    public class XmlSerializationHelper
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
    }
}
