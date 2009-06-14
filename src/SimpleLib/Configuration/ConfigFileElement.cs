using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration2
{
    public class ConfigFileElement : ConfigElement
    {
        [ConfigElement("type")]
        public string Type { get; set; }
        [ConfigElement("file")]
        public string File { get; set; }
    }
}
