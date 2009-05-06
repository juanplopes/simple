using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Simple.Threading
{
    //public class CriticalRegion : CriticalRegion<SqlLockToken>
    //{
    //    public const int DefaultWait = DefaultLockingProvider.DEFAULT_WAIT;
    //    public const int NoWait = 0;

    //    public CriticalRegion(string type, int id, int secondsToWait) : base(new DefaultLockingProvider(), type, id, secondsToWait) { }
    //    public CriticalRegion(string type, int id) : this(type, id, DefaultLockingProvider.DEFAULT_WAIT) { }

    //    public static CriticalRegion Begin(string type, int id, int secondsToWait)
    //    {
    //        return new CriticalRegion(type, id, secondsToWait);
    //    }

    //    public static CriticalRegion Begin(string type, int id)
    //    {
    //        return new CriticalRegion(type, id);
    //    }
    //}

    public abstract class CriticalRegion<PType, TType> : IDisposable
        where PType : ILockingProvider<TType>, new()
        where TType : ILockToken
    {
        protected TType LockToken { get; set; }
        protected ILockingProvider<TType> Provider { get; set; }

        protected CriticalRegion(string type, int id)
            : this(type, id, TimeoutValues.DefaultWait)
        {
        }

        protected CriticalRegion(
            string type, int id, int timeout)
        {
            Provider = new PType();
            LockToken = Provider.Lock(type, id, timeout);
        }

        public void End()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            Provider.Release(LockToken);
        }
    }
}
