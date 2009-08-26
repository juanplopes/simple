using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.ConfigSource
{
    public interface IConfiguratorInterface<T, R>
    {
        R FromFile(string file);
        R FromXml(string xml);
        R FromStream(Stream stream);
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

        public R FromFile(string file)
        {
            return Configurator(XmlConfig.LoadFile<T>(file));
        }
        public R FromXml(string xml)
        {
            return Configurator(XmlConfig.LoadXml<T>(xml));
        }
        public R FromStream(Stream stream)
        {
            return Configurator(XmlConfig.LoadStream<T>(stream));
        }
    }
}
