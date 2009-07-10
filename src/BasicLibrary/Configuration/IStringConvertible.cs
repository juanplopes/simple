using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Configuration
{
    public interface IStringConvertible
    {
        void LoadFromString(string value);
    }
}
