using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ConfigDictionaryKeyNameAttribute : NamedAttribute
    {
        public const string DEFAULT_KEY_NAME = "key";
        public ConfigDictionaryKeyNameAttribute(string name) : base(name) { }
    }
}
