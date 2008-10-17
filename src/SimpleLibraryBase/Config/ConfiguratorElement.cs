using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class ConfiguratorElement : PlainXmlConfigElement
    {
        [ConfigElement("type", Required=true)]
        public string Type { get; set; }
        [ConfigElement("runOnlyAtServer",Default=false)]
        public bool RunOnlyAtServer { get; set; }
    }
}
