using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace BasicLibrary.Configuration
{
    public abstract class AutoLocalizationConfigProvider<T> : IConfigProvider<T>
        where T : ConfigElement, new()
    {
        public abstract T Get(string location);
        
        [LocalizationProviderIgnore]
        public T Get()
        {
            return Get(AutoGetLocation());
        }

        [LocalizationProviderIgnore]
        protected string AutoGetLocation()
        {
            StackTrace stack = new StackTrace();
            MethodBase method; int currentFrameCounter = 1;
            do
            {
                method = stack.GetFrame(currentFrameCounter++).GetMethod();
            } while (Attribute.IsDefined(method, typeof(LocalizationProviderIgnoreAttribute)));

            return GetLocationFromMethod(method);
        }

        [LocalizationProviderIgnore]
        protected string GetLocationFromMethod(MethodBase callerMethod)
        {
            LocalizationProviderAttribute[] attribute;

            attribute = (LocalizationProviderAttribute[])callerMethod.GetCustomAttributes(typeof(LocalizationProviderAttribute), true);
            if (attribute.Length > 0) return attribute[0].Provider.GetLocalization(typeof(T));

            Type type = callerMethod.DeclaringType;
            attribute = (LocalizationProviderAttribute[])type.GetCustomAttributes(typeof(LocalizationProviderAttribute), true);
            if (attribute.Length > 0) return attribute[0].Provider.GetLocalization(typeof(T));

            Assembly assembly = type.Assembly;
            attribute = (LocalizationProviderAttribute[])assembly.GetCustomAttributes(typeof(LocalizationProviderAttribute), true);
            if (attribute.Length > 0) return attribute[0].Provider.GetLocalization(typeof(T));

            return null;

        }

    }
}
