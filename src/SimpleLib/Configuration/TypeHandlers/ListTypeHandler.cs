using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using Simple.Common;
using System.Xml;
using Simple.Reflection;

namespace Simple.Configuration.TypeHandlers
{
    public class ListTypeHandler : ConfigTypeHandler
    {
        public ListTypeHandler(LoadConfiguration config) : base(config) 
        {
            AlreadyDefined = new Dictionary<string, bool>();
        }

        protected Dictionary<string, bool> AlreadyDefined { get; set; }

        protected override bool IsMatch(Type type)
        {
            return typeof(IList).IsAssignableFrom(type) && type.GetGenericArguments().Length == 1;
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

            if (!AlreadyDefined.ContainsKey(element.Name)) ((IList)property.GetValue(ConfigInfo.Element, null)).Clear();

            ((IList)property.GetValue(ConfigInfo.Element, null)).Add(Resolver.FromXmlElement(element, property.PropertyType.GetGenericArguments()[0], null));

            AlreadyDefined[element.Name] = true;
        }

        public override void NotifyStartHandling()
        {
            base.NotifyStartHandling();
            AlreadyDefined.Clear();
        }

    }
}
