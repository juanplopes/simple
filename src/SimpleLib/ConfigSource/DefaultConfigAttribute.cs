using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple=false, Inherited=true)]
    public class DefaultConfigAttribute : Attribute
    {
        public object Key { get; protected set; }

        public DefaultConfigAttribute(object key)
        {
            this.Key = key;
        }
    }
}
