using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    public class DateTimeParser : IParser
    {
        public string Format { get; private set; }

        public DateTimeParser(string format)
        {
            Format = format;
        }

        #region IParser Members

        public object Parse(string value, Type type, IFormatProvider provider)
        {
            return DateTime.ParseExact(value, Format, provider);
        }

        #endregion
    }
}
