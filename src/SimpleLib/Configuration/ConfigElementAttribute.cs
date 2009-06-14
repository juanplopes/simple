using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration2
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ConfigElementAttribute : NamedAttribute
    {
        public ConfigElementAttribute(string name) : base(name) { }

        public object Default { get; set; }
        public bool Required { get; set; }
    }

    public enum InstanceType
    {
        New
    }

}
