using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Persistence;

namespace SimpleLibrary.Threading
{
    public abstract class SimpleInstanceState<T> : GenericInstanceState<T, NHLockingProvider, NHLockToken>
        where T:GenericInstanceState<T, NHLockingProvider, NHLockToken>,new()
    {
    }
}
