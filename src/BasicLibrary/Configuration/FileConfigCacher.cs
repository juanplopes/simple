using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Cache;
using BasicLibrary.Logging;
using System.Xml;
using System.IO;

namespace BasicLibrary.Configuration
{
    public class FileConfigCacher<T> : BaseCacher<ConfigIdentifier, T>
                where T : ConfigElement, new()
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
                    string fileContent = File.GetValue();
                    XmlElement xmlElement;
                    if (fileContent != null)
                    {
                        XmlDocument mainDocument = new XmlDocument();
                        mainDocument.LoadXml(fileContent);
                        xmlElement = mainDocument.DocumentElement;
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
                            Log("No exception thrown. Loading default xml string...");
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(Value.DefaultXmlString);
                            xmlElement = doc.DocumentElement;
                        }
                    }

                    Log("Loaded main document.");
                    Value.LoadFromElement(xmlElement);

                    foreach (XmlElement element in xmlElement.GetElementsByTagName(LocalizationTag))
                    {
                        if (element.Attributes[CountryAtribute] != null &&
                            string.Equals(element.Attributes[CountryAtribute].Value,
                            Identifier.Localization, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Log(string.Format("Loading localized value for {0}...", element.Attributes[CountryAtribute].Value));
                            Value.LoadFromElement(element);
                        }
                    }

                    Log("Locking element.");
                    Value.Lock();
                }
            }
            Log(sReturningCached);
            return Value;
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

        public override string GetFormattedId()
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
