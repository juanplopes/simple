using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BasicLibrary.Configuration.TypeHandlers
{
    public class EnumTypeHandler : AttributeAbleTypeHandler
    {
        public EnumTypeHandler(LoadConfiguration config) : base(config) { }

        protected override object GetFromXmlElement(XmlElement element, Type type, object currentValue)
        {
            return Enum.Parse(type, element.InnerText);
        }

        protected override bool IsMatch(Type type)
        {
            return typeof(Enum).IsAssignableFrom(type);
        }
    }
}
