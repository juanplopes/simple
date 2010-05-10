using System.IO;

namespace Simple.Config
{
    
    public class ConfiguratorInterface<T, R> : IConfiguratorInterface<T, R>
    {
        /// <summary>
        /// A delegate that should define how to act with some kind of loaded source.
        /// </summary>
        /// <param name="source">The source itself.</param>
        /// <returns>The return point.</returns>
        public delegate R ConfiguratorDelegate(IConfigSource<T> source);

        protected ConfiguratorDelegate Configurator { get; set; }

        /// <summary>
        /// Initializes the class with a instance of <see cref="ConfiguratorDelegate"/> instance.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public ConfiguratorInterface(ConfiguratorDelegate configurator)
        {
            Configurator = configurator;
        }

        /// <summary>
        /// Loads a configuration from instance.
        /// </summary>
        /// <param name="config">The configuration instance.</param>
        /// <returns>The return point.</returns>
        public R FromInstance(T config)
        {
            return Configurator(new DirectConfigSource<T>().Load(config));
        }
        
        /// <summary>
        /// Loads a configuration from xml file.
        /// </summary>
        /// <param name="file">The filename.</param>
        /// <returns>The return point.</returns>
        public R FromXmlFile(string file)
        {
            return Configurator(XmlConfig.LoadFile<T>(file));
        }
        /// <summary>
        /// Loads a configuration from xml file and XPath.
        /// </summary>
        /// <param name="file">The filename.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The return point.</returns>
        public R FromXmlFile(string file, string xPath)
        {
            return Configurator(XmlConfig.LoadFile<T>(file, xPath));
        }

        /// <summary>
        /// Loads a configuration from xml string.
        /// </summary>
        /// <param name="file">The string.</param>
        /// <returns>The return point.</returns>
        public R FromXmlString(string xml)
        {
            return Configurator(XmlConfig.LoadXml<T>(xml));
        }

        /// <summary>
        /// Loads a configuration from xml string and XPath.
        /// </summary>
        /// <param name="file">The string.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The return point.</returns>
        public R FromXmlString(string xml, string xPath)
        {
            return Configurator(XmlConfig.LoadXml<T>(xml, xPath));
        }

        /// <summary>
        /// Loads a configuration from xml stream.
        /// </summary>
        /// <param name="file">The stream.</param>
        /// <returns>The return point.</returns>
        public R FromStream(Stream stream)
        {
            return Configurator(XmlConfig.LoadStream<T>(stream));
        }

        /// <summary>
        /// Loads a configuration from xml stream and XPath.
        /// </summary>
        /// <param name="file">The stream.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The return point.</returns>
        public R FromStream(Stream stream, string xPath)
        {
            return Configurator(XmlConfig.LoadStream<T>(stream, xPath));
        }
    }
}
