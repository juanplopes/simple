using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration2
{
    public interface IConfigProvider<T> where T : IConfigElement, new()
    {
        T Get();
        T Get(string location);
    }
}
