using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleGenerator.Definitions
{
    [DefaultFile("Generator.config")]
    public class GeneratorConfig : ConfigRoot<GeneratorConfig>
    {
        [ConfigElement("rulesDirectory")]
        public string RulesDirectory { get; set; }

        [ConfigElement("domainDirectory")]
        public string DomainDirectory { get; set; }

        [ConfigElement("domainNamespace")]
        public string DomainNamespace { get; set; }

        [ConfigElement("rulesInterfacesNamespace")]
        public string RulesInterfacesNamespace { get; set; }

        [ConfigElement("rulesInterfacesGeneratedFile")]
        public string RulesInterfacesGeneratedFile { get; set; }
    }
}
