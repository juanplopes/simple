using System;
using System.Collections.Generic;
using System.Linq;


using System.Text;

namespace Simple.ConfigSource
{
    public delegate void HandleConfigExpired<T>(IConfigSource<T> source);

    public interface IConfigSource<T> : IDisposable
    {
        T Reload();
        event HandleConfigExpired<T> Expired;
    }

    public interface IConfigSource<T, Q> : IConfigSource<T>
    {
        T Load(Q input);
    }
}
