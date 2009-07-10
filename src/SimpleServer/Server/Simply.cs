using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Simple.DataAccess;
using NHibernate.Cfg;
using Simple.DataAccess.Context;

namespace Simple.Server
{
    public class Simply : ServerSimplyBase<Simply>
    {
    }

    public class ServerSimplyBase<F> : Client.ClientSimplyBase<F>, INHibernateFactory, IDataContextFactory
        where F : Client.ClientSimplyBase<F>, new()
    {
        #region DataAccess
        public Configuration NHConfiguration
        {
            get { return SessionManager.GetConfig(ConfigKey); }
        }
        public ISession OpenNHSession()
        {
            return SessionManager.OpenSession(ConfigKey);
        }
        #endregion


        #region IDataContextFactory Members

        public IDataContext EnterContext()
        {
            return DataContextFactory.Get(ConfigKey).EnterContext();
        }

        public IDataContext GetContext()
        {
            return DataContextFactory.Get(ConfigKey).GetContext();
        }

        #endregion
    }
}
