using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    public class DefaultParser : IParser
    {
        private class Nested
        {
            public static IParser Instance = new DefaultParser();
        }
        public static IParser Instance { get { return Nested.Instance; } }

        public DefaultParser() { }

        #region IParser Members

        public object Parse(string value, Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(value, type, provider);
        }

        #endregion
    }
}
