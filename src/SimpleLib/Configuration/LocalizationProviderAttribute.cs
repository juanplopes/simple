using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration2
{
    [global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class LocalizationProviderAttribute : Attribute 
    {
        public ILocalizationProvider Provider { get; private set; }

        public LocalizationProviderAttribute(Type type)
        {
            if (!typeof(ILocalizationProvider).IsAssignableFrom(type)) throw new InvalidOperationException("localization provider must inherit from ILocalizationProvider interface");
            if (type == null) type = typeof(NopLocalizationProvider);
            Provider = (ILocalizationProvider)Activator.CreateInstance(type);
        }
    }
}
