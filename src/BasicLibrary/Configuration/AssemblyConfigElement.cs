using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace BasicLibrary.Configuration
{
    public class AssemblyConfigElement : PlainXmlConfigElement
    {
        [ConfigElement("name", Default = null)]
        public string AssemblyName { get; set; }
        [ConfigElement("file", Default = null)]
        public string File { get; set; }

        public AssemblyName GetAssemblyName()
        {
            return new AssemblyName(AssemblyName);
        }

        public Assembly LoadAssembly()
        {
            try
            {
                return AppDomain.CurrentDomain.Load(GetAssemblyName());
            }
            catch
            {
                return Assembly.LoadFile(File);
            }
        }

        protected override void CheckConstraints()
        {
            base.CheckConstraints();
            if (File == null && File == AssemblyName) throw new InvalidConfigurationException("Either file or assembly name must be populated.");
        }
    }
}
