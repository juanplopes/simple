using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Persistence;

namespace SimpleLibrary.Threading
{
    public abstract class SimplePageController<T> : PageController<T, NHLockingProvider, NHLockToken>
        where T : PageController<T, NHLockingProvider, NHLockToken>,new()
    {
    }
}
