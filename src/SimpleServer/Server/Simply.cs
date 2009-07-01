using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Simple.DataAccess;

namespace Simple.Server
{
    public class Simply : Client.Simply
    {
        #region DataAccess
        public ISession GetSession()
        {
            return SessionManager.GetSession();
        }
        public ISession GetSession(object key)
        {
            return SessionManager.GetSession(key);
        }
        public ITransaction BeginTransaction()
        {
            return SessionManager.BeginTransaction();
        }
        public ITransaction BeginTransaction(object key)
        {
            return SessionManager.BeginTransaction(key);
        }
        #endregion


    }
}
