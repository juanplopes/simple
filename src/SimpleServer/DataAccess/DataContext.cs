using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.DataAccess
{
    public class DataContext : IDisposable
    {
        public Guid? Identifier { get; set; }

        public DataContext(Guid? identifier)
        {
            this.Identifier = identifier;
        }

        public static DataContext Enter()
        {
            return new DataContext(SessionManagerOld.LockThreadSessions());
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (SessionManagerOld.IsInitialized)
                SessionManagerOld.ReleaseThreadSessions(this.Identifier);
        }

        #endregion
    }
}
