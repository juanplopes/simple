using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Xml;
using BasicLibrary.Common;
using System.IO;
using BasicLibrary.Logging;

namespace BasicLibrary.Configuration
{
    public class FileConfigProvider<T> : AutoLocalizationConfigProvider<T>
        where T : ConfigElement, new()
    {

        protected SafeDictionary<string, string> ConfigFiles
        {
            get
            {
                ConfigFilesConfig config = ConfigFilesConfig.Get();
                return config.ConfigFiles;
            }
        }

        protected SafeDictionary<ConfigIdentifier, T> Cache = new SafeDictionary<ConfigIdentifier, T>();

        public static FileConfigProvider<T> Instance
        {
            get
            {
                return Nested.Provider;
            }
        }

        protected static class Nested
        {
            public static FileConfigProvider<T> Provider = new FileConfigProvider<T>();
        }

        [LocalizationProviderIgnore]
        public T Get(string file, string location)
        {
            ConfigIdentifier id = new ConfigIdentifier(file, location);
            return FileConfigCacher<T>.GetValue(id);
        }

        [LocalizationProviderIgnore]
        public override T Get(string location)
        {
            string file;

            if (!Attribute.IsDefined(typeof(T), typeof(ConfigFilesIgnoreAttribute)))
            {
                file = ConfigFiles[typeof(T).FullName];
                if (file != null) return Get(file, location);
            }

            file = DefaultFileAttribute.GetDefaultFile<T>();
            if (file != null) return Get(file, location);

            throw new InvalidOperationException("Cannot load " + typeof(T).Name + " from file. Config file not specified.");
        }
    }
}
