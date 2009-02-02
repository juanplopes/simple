using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    public interface IParser
    {
        object Parse(string value, Type type, IFormatProvider provider);
    }
}
