using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Globalization;

namespace Simple.Configuration.TypeHandlers
{
    public class BasicTypeHandler : AttributeAbleTypeHandler
    {
        public BasicTypeHandler(LoadConfiguration config) : base(config) { }
        protected override bool IsMatch(Type type)
        {
            return typeof(IConvertible).IsAssignableFrom(type);
        }

        protected override void ForEachProperty(PropertyInfo property, ConfigElementAttribute attribute)
        {
            base.ForEachProperty(property, attribute);
        }
    }
}
