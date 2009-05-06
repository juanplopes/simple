using System;
using System.Collections.Generic;

using System.Text;
using Simple.Persistence;

namespace Simple.Threading
{
    public abstract class SimplePageController<T> : PageController<T, NHLockingProvider, NHLockToken>
        where T : PageController<T, NHLockingProvider, NHLockToken>,new()
    {
    }
}
