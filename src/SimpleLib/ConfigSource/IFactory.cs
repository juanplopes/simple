using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public interface IFactory<T>
        where T : new()
    {
        bool Initialized { get; }
        void Init(IConfigSource<T> source);
        void ClearConfig();
    }
}
