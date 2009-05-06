using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration
{
    public interface IStringConvertible
    {
        void LoadFromString(string value);
    }
}
