using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Simple.DataAccess;
using NHibernate.Cfg;

namespace Simple.Server
{
    public class Simply : ServerSimplyBase<Simply>
    {
    }

    public class ServerSimplyBase<F> : Client.ClientSimplyBase<F>, INHibernateFactory
        where F:Client.ClientSimplyBase<F>, new()
    {
        #region DataAccess
        public Configuration Configuration
        {
            get { return SessionManager.GetConfig(ConfigKey); }
        }
        public ISession GetSession()
        {
            return SessionManager.GetSession(ConfigKey);
        }
        public ITransaction BeginTransaction()
        {
            return SessionManager.BeginTransaction(ConfigKey);
        }
        #endregion


    }
}
