using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Simple.Configuration
{
    [LocalizationProviderIgnore]
    public abstract class AutoLocalizationConfigProvider<T> : IConfigProvider<T>
        where T : IConfigElement, new()
    {
        public abstract T Get(string location);

        public T Get()
        {
            return Get(GetLocationFromAssembly(Assembly.GetEntryAssembly()));
        }

        protected string GetLocationFromAssembly(Assembly assembly)
        {
            if (assembly == null) return null;

            LocalizationProviderAttribute[] attribute = (LocalizationProviderAttribute[])assembly.GetCustomAttributes(typeof(LocalizationProviderAttribute), true);
            if (attribute.Length > 0) return attribute[0].Provider.GetLocalization(typeof(T));

            return null;

        }

    }
}
