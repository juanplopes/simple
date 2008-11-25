using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace BasicLibrary.Configuration.TypeHandlers
{
    public abstract class AttributeAbleTypeHandler : ElementTypeHandler
    {
        public AttributeAbleTypeHandler(LoadConfiguration config) : base(config) { }

        public virtual void Handle(XmlAttribute attribute)
        {
            ConfigInfo.NotifyLoad(attribute.Name);
            if (Properties.ContainsKey(attribute.Name))
            {
                PropertyInfo property = Properties[attribute.Name];

                property.SetValue(ConfigInfo.Element,
                    Resolver.GetFromXmlString(attribute.Value, property.PropertyType),
                    null);
            }
        }
    }
}
