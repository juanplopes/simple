using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public interface IWrappedConfigSource<T, A> : IConfigSource<T, IConfigSource<A>>
        where T:new()
        where A:new()
    {
    }
}
