using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.ConfigSource
{
    public interface IConfiguratorInterface<T, R>
    {
        R FromXmlFile(string file);
        R FromXmlFile(string file, string xPath);
        R FromXmlString(string xml);
        R FromXmlString(string xml, string xPath);
        R FromStream(Stream stream);
        R FromStream(Stream stream, string xPath);
        R FromInstance(T config);
    }

    public class ConfiguratorInterface<T, R> : IConfiguratorInterface<T, R>
    {
        public delegate R ConfiguratorDelegate(IConfigSource<T> source);

        protected ConfiguratorDelegate Configurator { get; set; }

        public ConfiguratorInterface(ConfiguratorDelegate configurator)
        {
            Configurator = configurator;
        }

        public R FromInstance(T config)
        {
            return Configurator(new DirectConfigSource<T>().Load(config));
        }

        public R FromXmlFile(string file)
        {
            return Configurator(XmlConfig.LoadFile<T>(file));
        }
        public R FromXmlFile(string file, string xPath)
        {
            return Configurator(XmlConfig.LoadFile<T>(file, xPath));
        }

        public R FromXmlString(string xml)
        {
            return Configurator(XmlConfig.LoadXml<T>(xml));
        }
        public R FromXmlString(string xml, string xPath)
        {
            return Configurator(XmlConfig.LoadXml<T>(xml, xPath));
        }

        public R FromStream(Stream stream)
        {
            return Configurator(XmlConfig.LoadStream<T>(stream));
        }
        public R FromStream(Stream stream, string xPath)
        {
            return Configurator(XmlConfig.LoadStream<T>(stream, xPath));
        }
    }
}
