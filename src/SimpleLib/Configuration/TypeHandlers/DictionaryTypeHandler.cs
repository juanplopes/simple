using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using Simple.Common;
using System.Xml;
using Simple.Reflection;
using Simple.Patterns;

namespace Simple.Configuration2.TypeHandlers
{
    public class DictionaryTypeHandler : ConfigTypeHandler
    {
        public DictionaryTypeHandler(LoadConfiguration config) : base(config) { }
        
        protected override bool IsMatch(Type type)
        {
            Type[] genericTypes = type.GetGenericArguments();            
            return typeof(IDictionary).IsAssignableFrom(type) && genericTypes.Length == 2 && typeof(IConvertible).IsAssignableFrom(genericTypes[0]);
        }

        protected override void ForEachProperty(PropertyInfo property, ConfigElementAttribute attribute)
        {
            base.ForEachProperty(property, attribute);

            Type[] genericParameters = property.PropertyType.GetGenericArguments();
            property.SetValue(ConfigInfo.Element, GenericActivator.CreateInstance(property.PropertyType.GetGenericTypeDefinition(), genericParameters, new object[0]), null);
        }

        protected override void HandleElement(XmlElement element)
        {
            PropertyInfo property = Properties[element.Name];
            ConfigDictionaryKeyNameAttribute configElement = Enumerable.GetFirst<ConfigDictionaryKeyNameAttribute>(property.GetCustomAttributes(typeof(ConfigDictionaryKeyNameAttribute), true));
            string keyName = (configElement != null) ? configElement.Name : ConfigDictionaryKeyNameAttribute.DEFAULT_KEY_NAME;
            string key = "";
            try
            {
                key = element.Attributes[keyName].Value;
            }
            catch (NullReferenceException)
            {
                throw new InvalidOperationException("Unable to acquire dictionary key");
            }

            Type[] types = property.PropertyType.GetGenericArguments();
            Type keyType = types[0];
            Type valueType = types[1];
            object objectKey = Resolver.FromString(key, keyType);

            ((IDictionary)property.GetValue(ConfigInfo.Element, null))[objectKey] =
                Resolver.FromXmlElement(element, valueType, ((IDictionary)property.GetValue(ConfigInfo.Element, null))[objectKey]);
        }
    }
}
