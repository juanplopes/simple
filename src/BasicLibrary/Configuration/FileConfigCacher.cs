using System;
using System.Collections.Generic;

using System.Text;
using Simple.Cache;
using Simple.Logging;
using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace Simple.Configuration
{
    public class FileConfigCacher<T> : BaseCacher<ConfigIdentifier, T>
                where T : IConfigElement, new()
    {
        public const string LocalizationTag = "localized";
        public const string CountryAtribute = "for";

        public override event CacheExpired<ConfigIdentifier> CacheExpiredEvent;

        public FileCacher File { get; set; }
        protected bool IsValid { get; set; }

        protected T Value { get; set; }

        public static Dictionary<ConfigIdentifier, FileConfigCacher<T>> CachedValues =
            new Dictionary<ConfigIdentifier, FileConfigCacher<T>>();

        public FileConfigCacher(ConfigIdentifier identifier)
        {
            Identifier = identifier;
            IsValid = false;
            File = FileCacher.GetCacher(Identifier.File);
            File.CacheExpiredEvent += new CacheExpired<string>(File_CacheExpiredEvent);
        }

        void File_CacheExpiredEvent(string identifier)
        {
            if (IsValid)
            {
                Log("Cache expired: file expired");
                IsValid = false;
                if (CacheExpiredEvent != null) CacheExpiredEvent.Invoke(Identifier);
                if (Value != null) Value.InvokeExpire();
            }
        }

        public override T GetValue()
        {
            lock (CachedValues)
            {
                Log(sTrying);
                if (!Validate())
                {
                    Log(sNotValid);
                    IsValid = true;

                    Value = new T();

                    bool loadDefaultFirst = DefaultFileAttribute.ShouldLoadDefaultFirst<T>();
                    if (loadDefaultFirst)
                    {
                        Log("Loading default element...");
                        LoadIntoElement(GetXmlFromString(Value.DefaultXmlString), true);
                    }
                    
                    string fileContent = File.GetValue();

                    if (fileContent != null)
                    {
                        Log("Loading file element...");
                        LoadIntoElement(GetXmlFromString(fileContent), false);
                    }
                    else
                    {
                        Log("Invalid loaded file. Deciding what to do...");
                        if (DefaultFileAttribute.ShouldThrowException<T>())
                        {
                            Log("Rethrowing exception.");
                            throw new FileNotFoundException("Couldn't load configuration file", Identifier.File);
                        }
                        else
                        {
                            Log("No exception thrown.");
                            if (!loadDefaultFirst)
                            {
                                Log("Loading default element...");
                                LoadIntoElement(GetXmlFromString(Value.DefaultXmlString), true);
                            }
                            else
                            {
                                Log("Default element already loaded. Skipping.");
                            }
                        }
                    }

                    Log("Locking element.");
                    (Value as IConfigElement).Lock();
                }
            }
            Log(sReturningCached);
            return Value;
        }

        protected XmlElement GetXmlFromString(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement xmlElement = doc.DocumentElement;
            return xmlElement;
        }

        protected void LoadIntoElement(XmlElement xml, bool isDefault)
        {
            Log(string.Format("Loaded {0}main document.", isDefault ? "DEFAULT " : ""));
            (Value as IConfigElement).LoadFromElement(xml);

            foreach (XmlElement element in xml.GetElementsByTagName(LocalizationTag))
            {
                if (element.Attributes[CountryAtribute] != null &&
                    string.Equals(element.Attributes[CountryAtribute].Value,
                    Identifier.Localization, StringComparison.InvariantCultureIgnoreCase))
                {
                    Log(string.Format("Loading {0}localized value for {1}...", isDefault ? "DEFAULT " : "", element.Attributes[CountryAtribute].Value));
                    (Value as IConfigElement).LoadFromElement(element);
                }
            }

        }

        public static FileConfigCacher<T> GetCacher(ConfigIdentifier identifier)
        {
            lock (CachedValues)
            {
                try
                {
                    FileConfigCacher<T> cacher = CachedValues[identifier];
                    return cacher;
                }
                catch (KeyNotFoundException)
                {
                    FileConfigCacher<T> cacher = new FileConfigCacher<T>(identifier);
                    CachedValues[identifier] = cacher;
                    return cacher;
                }
            }
        }

        public static T GetValue(ConfigIdentifier identifier)
        {
            lock (CachedValues)
            {
                FileConfigCacher<T> cacher = GetCacher(identifier);
                return cacher.GetValue();
            }
        }


        public override bool Validate()
        {
            return IsValid;
        }

        protected override string GetFormattedId()
        {
            string typeName = typeof(T).Name;
            if (this.Identifier.Localization != null)
            {
                typeName += "[" + this.Identifier.Localization + "]";
            }
            return typeName;
        }
    }
}
