using System;
using System.Collections.Generic;

using System.Text;
using Simple.Persistence;

namespace Simple.Threading
{
    public abstract class SimpleInstanceState<T> : GenericInstanceState<T, NHLockingProvider, NHLockToken>
        where T:GenericInstanceState<T, NHLockingProvider, NHLockToken>,new()
    {
    }
}
