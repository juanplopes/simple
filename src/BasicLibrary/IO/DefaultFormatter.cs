using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    public class DefaultFormatter : IFormatter
    {
        private class Nested
        {
            public static IFormatter Instance = new DefaultFormatter();
        }


        public static IFormatter Instance { get { return Nested.Instance; } }

        public string ObjectFormat { get; set; }

        public DefaultFormatter() : this(null) { }
        public DefaultFormatter(string format)
        {
            ObjectFormat = format;
        }

        public virtual object Parse(string value, Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(value, type, provider);
        }


        public virtual string Format(object value, IFormatProvider provider)
        {
            if (ObjectFormat == null)
                return Convert.ToString(value, provider);
            else
                return string.Format(provider, "{0:" + ObjectFormat + "}", value);
        }

    }
}
