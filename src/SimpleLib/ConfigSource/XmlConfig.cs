using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public static class XmlConfig
    {
        public static IConfigSource<T> LoadFile<T>(string fileName)
            where T : new()
        {
            return new XmlFileConfigSource<T>().LoadFile(fileName);
        }
    }
}
