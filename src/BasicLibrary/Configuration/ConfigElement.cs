using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Globalization;
using BasicLibrary.Common;
using BasicLibrary.Configuration.TypeHandlers;
using System.Configuration;
using BasicLibrary.Logging;
using log4net;

namespace BasicLibrary.Configuration
{
    public class ConfigElement : IConfigElement
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public event EventHandler OnExpire;
        void IConfigElement.InvokeExpire()
        {
            if (OnExpire != null) OnExpire(this, new EventArgs());
        }

        protected LoadConfiguration ConfigInfo { get; set; }
        public IList<XmlElement> XmlElements { get; set; }

        protected ConfigElement()
        {
            this.LoadDefaults();
            ConfigInfo = new LoadConfiguration(this);
            XmlElements = new List<XmlElement>();

            ConfigInfo.AddHandler(new EnumTypeHandler(ConfigInfo));
            ConfigInfo.AddHandler(new BasicTypeHandler(ConfigInfo));
            ConfigInfo.AddHandler(new NullableTypeHandler(ConfigInfo));
            ConfigInfo.AddHandler(new ByteArrayTypeHandler(ConfigInfo));
            ConfigInfo.AddHandler(new ComplexTypeHandler(ConfigInfo));
            ConfigInfo.AddHandler(new DictionaryTypeHandler(ConfigInfo));
            ConfigInfo.AddHandler(new ListTypeHandler(ConfigInfo));
            ConfigInfo.EnsureThatAllAreHandled();
        }

        void IConfigElement.Lock()
        {
            ConfigInfo.Lock();
            foreach (PropertyInfo property in ConfigInfo.Attributes.Keys)
            {
                object value = property.GetValue(this, null);
                if (value != null && value is IConfigElement)
                {
                    (value as IConfigElement).Lock();
                }
            }

        }

        void IConfigElement.NotifyLoad(string elementName)
        {
            ConfigInfo.NotifyLoad(elementName);
        }

        void IConfigElement.LoadFromElement(IConfigElement element)
        {
            foreach (XmlElement xmlElement in element.XmlElements)
            {
                (this as IConfigElement).LoadFromElement(xmlElement);
            }
            (this as IConfigElement).Lock();
        }

        void IConfigElement.LoadFromElement(XmlElement element)
        {
            if (ConfigInfo == null) throw new InvalidDataException("ConfigElement default constructor must inherit from base default constructor");
            Dictionary<string, bool> alreadyDefined = new Dictionary<string, bool>();

            Logger.DebugFormat("Reading '{0}' into '{1}'...", element.Name, this.GetType().Name);

            ConfigInfo.NotifyStartHandling();
            foreach (XmlAttribute attribute in element.Attributes)
            {
                ConfigInfo.Handle(attribute);
            }

            foreach (XmlNode node in element) if (node is XmlElement)
                {
                    XmlElement childElement = (XmlElement)node;

                    ConfigInfo.Handle(childElement);
                }


            CheckConstraints();
            XmlElements.Add(element);
        }

        protected virtual void LoadDefaults() { }

        protected virtual void CheckConstraints() { }

        public virtual string DefaultXmlString
        {
            get { throw new InvalidOperationException("DefaultXmlString not defined!"); }
        }
    }
}
