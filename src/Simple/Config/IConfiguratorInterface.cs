using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Config
{
    public interface IConfiguratorInterface<T, R>
    {
        /// <summary>
        /// Loads a configuration from xml file.
        /// </summary>
        /// <param name="file">The filename.</param>
        /// <returns>The return point.</returns>
        R FromXmlFile(string file);

        /// <summary>
        /// Loads a configuration from xml file and XPath.
        /// </summary>
        /// <param name="file">The filename.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The return point.</returns>
        R FromXmlFile(string file, string xPath);

        /// <summary>
        /// Loads a configuration from xml string.
        /// </summary>
        /// <param name="file">The string.</param>
        /// <returns>The return point.</returns>
        R FromXmlString(string xml);

        /// <summary>
        /// Loads a configuration from xml string and XPath.
        /// </summary>
        /// <param name="file">The string.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The return point.</returns>
        R FromXmlString(string xml, string xPath);

        /// <summary>
        /// Loads a configuration from xml stream.
        /// </summary>
        /// <param name="file">The stream.</param>
        /// <returns>The return point.</returns>
        R FromStream(Stream stream);
        /// <summary>
        /// Loads a configuration from xml stream and XPath.
        /// </summary>
        /// <param name="file">The stream.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>The return point.</returns>
        R FromStream(Stream stream, string xPath);

        /// <summary>
        /// Loads a configuration from instance.
        /// </summary>
        /// <param name="config">The configuration instance.</param>
        /// <returns>The return point.</returns>
        R FromInstance(T config);
    }

}
