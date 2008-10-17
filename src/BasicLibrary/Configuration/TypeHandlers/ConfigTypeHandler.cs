using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BasicLibrary.Common;
using System.Xml;

namespace BasicLibrary.Configuration.TypeHandlers
{
    public abstract class ConfigTypeHandler
    {
        protected Dictionary<string, PropertyInfo> Properties { get; set; }
        protected LoadConfiguration ConfigInfo { get; set; }
        protected ConfigElement Element { get { return ConfigInfo.Element; } }
        protected ChildTypeResolver Resolver { get; set; }

        public ConfigTypeHandler(LoadConfiguration config)
        {
            Properties = new Dictionary<string, PropertyInfo>();
            Resolver = new ChildTypeResolver();
            ConfigInfo = config;
            LoadAllProperties();
        }

        public virtual void NotifyStartHandling() {}

        public void Handle(XmlElement element)
        {
            if (Properties.ContainsKey(element.Name))
            {
                ConfigInfo.NotifyLoad(Properties[element.Name]);
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
        }

        protected abstract bool IsMatch(Type type);
    }
}
