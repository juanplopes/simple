using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class BusinessElement : ConfigElement
    {
        [ConfigElement("interfaceAssembly", Required = true)]
        public AssemblyConfigElement InterfaceAssembly { get; set; }
        [ConfigElement("serverAssembly", Required = true)]
        public AssemblyConfigElement ServerAssembly { get; set; }
        [ConfigElement("rulesFactoryType", Default = InstanceType.New)]
        public List<TypeConfigElement> RulesFactories { get; set; }
        [ConfigElement("filters", Default = InstanceType.New)]
        public FiltersElement Filters { get; set; }
        [ConfigElement("exceptionHandling", Default = InstanceType.New)]
        public ExceptionHandlingElement ExceptionHandling { get; set; }
    }
}
