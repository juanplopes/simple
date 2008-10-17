using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Threading;
using BasicLibrary.Common;

namespace BasicLibrary.Persistence
{
    [Serializable]
    public abstract class PersistedInstanceState<T> : GenericInstanceState<T, DefaultLockingProvider, SqlLockToken>
        where T : PersistedInstanceState<T>, new()
    {
        public static T Get(int id)
        {
            return Get(id, DefaultLockingProvider.DEFAULT_WAIT);
        }
    }
}
