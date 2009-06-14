using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration2;

namespace Simple.Config
{
    public class BusinessElement : ConfigElement
    {
        [ConfigElement("contractsAssembly", Required = true)]
        public AssemblyConfigElement ContractsAssembly { get; set; }
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
