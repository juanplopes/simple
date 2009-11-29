using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Config
{
    public interface IFactory<T>
    {
        bool Initialized { get; }
        void Init(IConfigSource<T> source);
        void Clear();
    }
}
