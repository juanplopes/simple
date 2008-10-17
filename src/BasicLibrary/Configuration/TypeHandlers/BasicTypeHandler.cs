using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Globalization;

namespace BasicLibrary.Configuration.TypeHandlers
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
            object def = null;

            if ((InstanceType.New.Equals(attribute.Default)))
                def = Activator.CreateInstance(property.PropertyType);
            else
                def = attribute.Default;

            property.SetValue(ConfigInfo.Element, def, null);
        }
    }
}
