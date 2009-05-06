using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration
{
    public class ConfigGroupElement : ConfigElement
    {
        [ConfigElement("path")]
        public string Path { get; set; }

        [ConfigElement("configFile")]
        public List<ConfigFileElement> Files { get; set; }
    }
}
