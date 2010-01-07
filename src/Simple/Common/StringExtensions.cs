using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Common
{
    public static class StringExtensions
    {
        public static string AsFormat(this string format, params object[] arg0)
        {
            return string.Format(format, arg0);
        }

        public static string AsFormat(this string format, IFormatProvider provider, params object[] arg0)
        {
            return string.Format(provider, format, arg0);
        }
    }
}
