using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Configuration
{
    public interface IConfigProvider<T> where T : ConfigElement, new()
    {
        T Get();
        T Get(string location);
    }
}
