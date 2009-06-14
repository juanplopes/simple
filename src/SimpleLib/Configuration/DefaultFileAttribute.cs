using System;
using System.Collections.Generic;
using System.Text;
using Simple.Common;

namespace Simple.Configuration2
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class DefaultFileAttribute : Attribute
    {
        public bool ThrowException { get; set; }
        public string DefaultContent { get; set; }
        public bool LoadDefaultFirst { get; set; }

        public DefaultFileAttribute(string defaultFile) : this(defaultFile, true) { }

        public DefaultFileAttribute(string defaultFile, bool throwException)
        {
            this.DefaultFile = defaultFile;
            this.ThrowException = throwException;
        }

        public string DefaultFile { get; set; }

        public static string GetDefaultFile<T>() where T : IConfigElement
        {
            DefaultFileAttribute attribute = Enumerable.GetFirst<DefaultFileAttribute>(typeof(T).GetCustomAttributes(typeof(DefaultFileAttribute), false));
            if (attribute == null) return null;
            return attribute.DefaultFile;
        }

        public static bool ShouldThrowException<T>() where T : IConfigElement
        {
            DefaultFileAttribute attribute = Enumerable.GetFirst<DefaultFileAttribute>(typeof(T).GetCustomAttributes(typeof(DefaultFileAttribute), false));
            if (attribute == null) return true;
            return attribute.ThrowException;
        }

        public static bool ShouldLoadDefaultFirst<T>() where T : IConfigElement
        {
            DefaultFileAttribute attribute = Enumerable.GetFirst<DefaultFileAttribute>(typeof(T).GetCustomAttributes(typeof(DefaultFileAttribute), false));
            if (attribute == null) return true;
            return attribute.LoadDefaultFirst;
        }
    }
}
