using System;
using System.Collections.Generic;
using System.Linq;


using System.Text;

namespace Simple.ConfigSource
{
    public delegate void HandleConfigExpired<T>(IConfigSource<T> source);

    public interface IConfigSource<T>
    {
        T Load();
        event HandleConfigExpired<T> Expired;
    }
}
