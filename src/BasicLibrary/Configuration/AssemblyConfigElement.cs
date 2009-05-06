using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Configuration;

namespace Simple.Configuration
{
    public class AssemblyConfigElement : PlainXmlConfigElement, IStringConvertible
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

        void IStringConvertible.LoadFromString(string value)
        {
            AssemblyName = value;
            (this as IConfigElement).NotifyLoad("name");
        }

    }
}
