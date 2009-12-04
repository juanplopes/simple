using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Config
{
    /// <summary>
    /// Provides helper methods for creating <see cref="XmlConfigSource<T>"/> instances.
    /// </summary>
    public static class XmlConfig
    {
        /// <summary>
        /// Creates a <see cref="IConfigSource<T>"/> from Xml stream.
        /// </summary>
        /// <typeparam name="T">The config type.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>The config source.</returns>
        public static IConfigSource<T> LoadStream<T>(Stream stream)
        {
            return new XmlConfigSource<T>().Load(stream);
        }

        /// <summary>
        /// Creates a <see cref="IConfigSource<T>"/> from Xml stream and XPath.
        /// </summary>
        /// <typeparam name="T">The config type.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The config source.</returns>
        public static IConfigSource<T> LoadStream<T>(Stream stream, string xPath)
        {
            return new XmlConfigSource<T>().Load(new XPathParameter<Stream>(stream, xPath));
        }

        /// <summary>
        /// Creates a <see cref="IConfigSource<T>"/> from Xml string.
        /// </summary>
        /// <typeparam name="T">The config type.</typeparam>
        /// <param name="xml">The xml.</param>
        /// <returns>The config source.</returns>
        public static IConfigSource<T> LoadXml<T>(string xml)
        {
            return new XmlConfigSource<T>().Load(xml);
        }

        /// <summary>
        /// Creates a <see cref="IConfigSource<T>"/> from Xml string and XPath.
        /// </summary>
        /// <typeparam name="T">The config type.</typeparam>
        /// <param name="xml">The xml.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The config source.</returns>
        public static IConfigSource<T> LoadXml<T>(string xml, string xPath)
        {
            return new XmlConfigSource<T>().Load(new XPathParameter<string>(xml, xPath));
        }

        /// <summary>
        /// Creates a <see cref="IConfigSource<T>"/> from Xml file.
        /// </summary>
        /// <typeparam name="T">The config type.</typeparam>
        /// <param name="fileName">The file.</param>
        /// <returns>The config source.</returns>
        public static IConfigSource<T> LoadFile<T>(string fileName)
        {
            return new XmlFileConfigSource<T>().LoadFile(fileName);
        }

        /// <summary>
        /// Creates a <see cref="IConfigSource<T>"/> from Xml file and XPath.
        /// </summary>
        /// <typeparam name="T">The config type.</typeparam>
        /// <param name="fileName">The file.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The config source.</returns>
        public static IConfigSource<T> LoadFile<T>(string fileName, string xPath)
        {
            return new XmlFileConfigSource<T>().LoadFile(fileName, xPath);
        }

    }
}
