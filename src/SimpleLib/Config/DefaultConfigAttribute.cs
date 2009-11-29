using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple.Config
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple=false, Inherited=true)]
    public class DefaultConfigAttribute : Attribute
    {
        public object Key { get; protected set; }

        public DefaultConfigAttribute(object key)
        {
            this.Key = key;
        }

        public static object GetKey(Type type)
        {
            DefaultConfigAttribute attr = AttributeCache.Do.First<DefaultConfigAttribute>(type);
            object key = attr == null ? null : attr.Key;
            return key;
        }
    }
}
