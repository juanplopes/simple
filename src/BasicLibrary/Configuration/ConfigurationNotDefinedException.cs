using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BasicLibrary.Configuration
{
    public class ConfigurationNotDefinedException : Exception
    {
        public ConfigurationNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public ConfigurationNotDefinedException() { }
        public ConfigurationNotDefinedException(string path) : base("Configuration not defined for: " + path) { }
    }
}
