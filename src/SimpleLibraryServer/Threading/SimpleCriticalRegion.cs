using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Threading;

namespace SimpleLibrary.Threading
{
    public class SimpleCriticalRegion : CriticalRegion<NHLockingProvider, NHLockToken>
    {
        public SimpleCriticalRegion(string type, int id, int timeout)
            : base(type, id, timeout)
        {

        }

        public SimpleCriticalRegion(string type, int id)
            : base(type, id)
        {

        }

        public static SimpleCriticalRegion Begin(string type, int id)
        {
            return new SimpleCriticalRegion(type, id);
        }

        public static SimpleCriticalRegion Begin(String type, int id, int timeout)
        {
            return new SimpleCriticalRegion(type, id, timeout);
        }
    }
}
