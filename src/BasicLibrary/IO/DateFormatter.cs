using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    public class DateFormatter : IFormatter
    {
        public string DateFormat { get; private set; }

        public DateFormatter(string format)
        {
            DateFormat = format;
        }

        #region IParser Members

        

        #endregion

        #region IFormatter Members


        public string Format(object value, IFormatProvider provider)
        {
            return ((DateTime)value).ToString(DateFormat, provider);
        }

        public object Parse(string value, Type type, IFormatProvider provider)
        {
            return DateTime.ParseExact(value, DateFormat, provider);
        }

        #endregion
    }
}
