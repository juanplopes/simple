using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Configuration;

namespace Simple.Configuration.TypeHandlers
{
    public class ChildTypeResolver
    {
        public static ChildTypeResolver Instance { get { return Nested._instance; } }

        protected class Nested
        {
            public static ChildTypeResolver _instance = new ChildTypeResolver();
        }

        public static object Get(string value, Type type)
        {
            return Instance.FromString(value, type);
        }

        public static T Get<T>(string value)
        {
            return (T)Instance.FromString(value, typeof(T));
        }

        public static object Get(XmlElement element, Type type, object current)
        {
            return Instance.FromXmlElement(element, type, current);

        }

        public static T Get<T>(XmlElement element, T current)
        {
            return (T)Instance.FromXmlElement(element, typeof(T), current);
        }

        public object FromXmlElement(XmlElement element, Type type, object currentValue)
        {
            if (typeof(IConfigElement).IsAssignableFrom(type))
            {
                IConfigElement configElement = (currentValue != null) ? (IConfigElement)currentValue : Activator.CreateInstance(type) as IConfigElement;
                configElement.LoadFromElement(element);
                if (element.ChildNodes.Count == 1 && element.ChildNodes[0].NodeType == XmlNodeType.Text)
                    if (configElement is IStringConvertible)
                        (configElement as IStringConvertible).LoadFromString(element.InnerText);
                return configElement;
            }
            else
            {
                return FromString(element.InnerText, type);
            }
        }

        public object FromString(string value, Type type)
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
                return FromString(value, type.GetGenericArguments()[0]);
            }
            else if (typeof(IStringConvertible).IsAssignableFrom(type))
            {
                IStringConvertible obj = (IStringConvertible)Activator.CreateInstance(type);
                obj.LoadFromString(value);
                return obj;
            }
            else if (typeof(IConfigElement).IsAssignableFrom(type))
            {
                throw new InvalidOperationException("An IConfigElement has been passed, but it does't implement IStringConvertible: " + type.ToString());
            }
            else
            {
                throw new InvalidConfigurationException("invalid property type: " + type.ToString());
            }
            
        }
    }
}
