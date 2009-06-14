using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Simple.Common;
using System.Configuration;
using System.Xml;
using System.Collections;
using Simple.Logging;
using log4net;

namespace Simple.Configuration2.TypeHandlers
{
    public class LoadConfiguration
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        protected Dictionary<PropertyInfo, ConfigTypeHandler> Handlers { get; set; }
        public Dictionary<string, bool> ParentList { get; set; }
        public Dictionary<PropertyInfo, ConfigElementAttribute> Attributes { get; set; }
        public List<ConfigTypeHandler> HandlerList { get; set; }
        public int HandledItems { get; protected set; }
        public Dictionary<string, bool> IsLoaded { get; set; }
        public Dictionary<string, ConfigTypeHandler> HandlerMapping { get; set; }
        public bool IsLocked { get; set; }

        public IConfigElement Element { get; set; }

        public LoadConfiguration(IConfigElement element)
        {
            Element = element;
            Handlers = new Dictionary<PropertyInfo, ConfigTypeHandler>();
            ParentList = new Dictionary<string, bool>();
            Attributes = new Dictionary<PropertyInfo, ConfigElementAttribute>();
            HandlerList = new List<ConfigTypeHandler>();
            IsLoaded = new Dictionary<string, bool>();
            HandlerMapping = new Dictionary<string, ConfigTypeHandler>();
            IsLocked = false;

            HandledItems = 0;

            foreach (PropertyInfo property in Element.GetType().GetProperties())
            {
                ConfigElementAttribute configElement = Enumerable.GetFirst<ConfigElementAttribute>(property.GetCustomAttributes(typeof(ConfigElementAttribute), true));
                if (configElement != null)
                {
                    ConfigAcceptsParentAttribute[] parentList = (ConfigAcceptsParentAttribute[])property.GetCustomAttributes(typeof(ConfigAcceptsParentAttribute), true);

                    foreach (ConfigAcceptsParentAttribute parent in parentList)
                    {
                        ParentList[parent.Name] = true;
                    }

                    Attributes[property] = configElement;
                    Handlers[property] = null;
                }
            }

        }
        public void NotifyLoad(string elementName)
        {
            IsLoaded[elementName] = true;
        }

        public void CheckRequiredProperties()
        {
            foreach (KeyValuePair<PropertyInfo, ConfigElementAttribute> pair in Attributes)
            {
                if (pair.Value.Required && !IsLoaded.ContainsKey(pair.Value.Name))
                {
                    throw new InvalidOperationException("Cannot lock until all required properties are loaded. Missing property: " + pair.Value.Name);
                }
            }
        }

        public void Lock()
        {
            CheckRequiredProperties();
            Logger.DebugFormat("{0}: All required properties are loaded. Locking...", Element.GetType().Name);
            foreach (var item in Attributes)
            {
                Logger.DebugFormat("  element '{0}' locked to '{1}' with type {2}", item.Value.Name, item.Key.Name, item.Key.PropertyType.Name);
            }
            IsLocked = true;
        }

        public void AddHandler(ConfigTypeHandler handler)
        {
            HandlerList.Add(handler);
        }

        public void NotifyStartHandling()
        {
            HandlerList.ForEach(x => x.NotifyStartHandling());
        }

        public void Handle(XmlElement element)
        {
            if (IsLocked) throw new InvalidOperationException("Configuration is already locked");
            

            HandlerList.ForEach(x => x.Handle(element));
            if (ParentList.ContainsKey(element.Name))
            {
                Logger.DebugFormat("Going deep the to the parent list {0}...", element.Name);
                (Element as IConfigElement).LoadFromElement(element);
            }
        }

        public void Handle(XmlAttribute attribute)
        {
            if (IsLocked) throw new InvalidOperationException("Configuration is already locked");

            foreach (ConfigTypeHandler handler in HandlerList)
            {
                if (handler is AttributeAbleTypeHandler)
                {
                    (handler as AttributeAbleTypeHandler).Handle(attribute);
                }
            }
        }

        public bool TryRegisterHandler(PropertyInfo property, ConfigTypeHandler handler)
        {
            if (Handlers[property] == null)
            {
                Handlers[property] = handler;
                HandledItems++;
                return true;
            }
            return false;
        }

        public void EnsureThatAllAreHandled()
        {
            if (HandledItems < Attributes.Count)
            {
                throw new InvalidConfigurationException("all properties must be handled");
            }
        }

    }
}
