using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    public interface IFormatter
    {
        object Parse(string value, Type type, IFormatProvider provider);
        string Format(object value, IFormatProvider provider);
    }
}
