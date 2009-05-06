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
            return new DataContext(SessionManager.LockThreadSessions());
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (SessionManager.IsInitialized)
                SessionManager.ReleaseThreadSessions(this.Identifier);
        }

        #endregion
    }
}
