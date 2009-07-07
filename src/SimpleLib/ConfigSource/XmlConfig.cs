using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.ConfigSource
{
    public static class XmlConfig
    {
        public static IConfigSource<T> LoadStream<T>(Stream stream)
        {
            return new XmlConfigSource<T>().Load(stream);
        }

        public static IConfigSource<T> LoadXml<T>(string xml)
        {
            return new XmlConfigSource<T>().Load(xml);
        }

        public static IConfigSource<T> LoadFile<T>(string fileName)
        {
            return new XmlFileConfigSource<T>().LoadFile(fileName);
        }
    }
}
