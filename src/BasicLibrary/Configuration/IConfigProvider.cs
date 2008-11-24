using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Configuration
{
    public interface IConfigProvider<T> where T : IConfigElement, new()
    {
        T Get();
        T Get(string location);
    }
}
