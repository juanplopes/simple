using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class BusinessElement : ConfigElement
    {
        [ConfigElement("interfaceAssembly", Required=true)]
        public string InterfaceAssembly { get; set; }
        [ConfigElement("serverAssembly", Required = true)]
        public string ServerAssembly { get; set; }
    }
}
