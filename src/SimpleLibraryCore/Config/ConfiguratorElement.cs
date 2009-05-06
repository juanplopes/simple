using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration;

namespace Simple.Config
{
    public enum SideType
    {
        Client,
        Server,
        Both
    }

    public class ConfiguratorElement : TypeConfigElement
    {
        [ConfigElement("runAt",Default=SideType.Both)]
        public SideType RunAt { get; set; }
    }
}
