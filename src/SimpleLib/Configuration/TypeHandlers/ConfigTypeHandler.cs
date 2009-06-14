using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Simple.Common;
using System.Xml;

namespace Simple.Configuration2.TypeHandlers
{
    public abstract class ConfigTypeHandler
    {
        protected Dictionary<string, PropertyInfo> Properties { get; set; }
        protected LoadConfiguration ConfigInfo { get; set; }
        protected IConfigElement Element { get { return ConfigInfo.Element; } }
        protected ChildTypeResolver Resolver { get; set; }

        public ConfigTypeHandler(LoadConfiguration config)
        {
            Properties = new Dictionary<string, PropertyInfo>();
            Resolver = ChildTypeResolver.Instance;
            ConfigInfo = config;
            LoadAllProperties();
        }

        public virtual void NotifyStartHandling() {}

        public void Handle(XmlElement element)
        {
            if (Properties.ContainsKey(element.Name))
            {
                ConfigInfo.NotifyLoad(element.Name);
                HandleElement(element);
            }
        }

        protected abstract void HandleElement(XmlElement element);

        protected void LoadAllProperties()
        {
            foreach (KeyValuePair<PropertyInfo, ConfigElementAttribute> pair in ConfigInfo.Attributes)
            {
                if (IsMatch(pair.Key.PropertyType))
                {
                    ForEachProperty(pair.Key, pair.Value);
                    ConfigInfo.TryRegisterHandler(pair.Key, this);
                }
            }
        }

        protected virtual void ForEachProperty(PropertyInfo property, ConfigElementAttribute attribute)
        {
            Properties[attribute.Name] = property;
            object def = null;

            if ((InstanceType.New.Equals(attribute.Default)))
                def = Activator.CreateInstance(property.PropertyType);
            else
                def = attribute.Default;

            property.SetValue(ConfigInfo.Element, def, null);
        }

        protected abstract bool IsMatch(Type type);
    }
}
