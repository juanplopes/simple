using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class ConfiguratorElement : TypeConfigElement
    {
        [ConfigElement("runOnlyAtServer",Default=false)]
        public bool RunOnlyAtServer { get; set; }
    }
}
