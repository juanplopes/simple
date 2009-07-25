using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.DataAccess.Context;
using NHibernate;

namespace Simple
{
    public static class DataContextSimplyExtensions
    {
        public static IDataContext EnterContext(this Simply simple)
        {
            return DataContextFactory.Get(simple.ConfigKey).EnterContext();
        }
        public static IDataContext GetContext(this Simply simple)
        {
            return DataContextFactory.Get(simple.ConfigKey).GetContext();
        }
        public static ISession GetSession(this Simply simple)
        {
            return DataContextFactory.Get(simple.ConfigKey).GetSession();
        }
        public static ISession NewSession(this Simply simple)
        {
            return DataContextFactory.Get(simple.ConfigKey).NewSession();
        }

    }
}
