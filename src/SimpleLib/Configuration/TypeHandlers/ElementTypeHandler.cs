using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;

namespace Simple.Configuration2.TypeHandlers
{ 
    public abstract class ElementTypeHandler : ConfigTypeHandler
    {
        public ElementTypeHandler(LoadConfiguration config) : base(config) { }

        protected override void HandleElement(XmlElement element)
        {
            PropertyInfo property = Properties[element.Name];

            property.SetValue(ConfigInfo.Element,
                GetFromXmlElement(
                    element,
                    property.PropertyType,
                    property.GetValue(ConfigInfo.Element, null)),
                null);
        }

        protected virtual object GetFromXmlElement(XmlElement element, Type type, object currentValue)
        {
            return Resolver.FromXmlElement(element, type, currentValue);
        }
    }
}
