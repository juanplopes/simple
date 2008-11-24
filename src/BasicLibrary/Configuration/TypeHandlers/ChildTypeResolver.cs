using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Configuration;

namespace BasicLibrary.Configuration.TypeHandlers
{
    public class ChildTypeResolver
    {
        public object GetFromXmlElement(XmlElement element, Type type, object currentValue)
        {
            if (typeof(IConfigElement).IsAssignableFrom(type))
            {
                IConfigElement configElement = (currentValue != null) ? (IConfigElement)currentValue : Activator.CreateInstance(type) as IConfigElement;
                configElement.LoadFromElement(element);
                return configElement;
            }
            else
            {
                return GetFromXmlString(element.InnerText, type);
            }
        }

        public object GetFromXmlString(string value, Type type)
        {
            if (typeof(Enum).IsAssignableFrom(type))
            {
                return Enum.Parse(type, value);
            }
            else if (typeof(byte[]).IsAssignableFrom(type))
            {
                return Convert.FromBase64String(value);
            }
            else if (typeof (IConvertible).IsAssignableFrom(type))
            {
                return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            }
            else if (type.IsGenericType && typeof(Nullable<>).IsAssignableFrom(type.GetGenericTypeDefinition()) && typeof(IConvertible).IsAssignableFrom(type.GetGenericArguments()[0]))
            {
                return GetFromXmlString(value, type.GetGenericArguments()[0]);
            }
            else
            {
                throw new InvalidConfigurationException("invalid property type: " + type.ToString());
            }
            
        }
    }
}
