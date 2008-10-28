using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace BasicLibrary.Threading
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

    public class CriticalRegion<TokenType> : IDisposable
        where TokenType : ILockToken
    {
        protected TokenType LockToken { get; set; }
        protected ILockingProvider<TokenType> Provider { get; set; }

        protected CriticalRegion(ILockingProvider<TokenType> provider, string type, int id, int secondsToWait)
        {
            Provider = provider;
            LockToken = Provider.Lock(type, id, secondsToWait);
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
