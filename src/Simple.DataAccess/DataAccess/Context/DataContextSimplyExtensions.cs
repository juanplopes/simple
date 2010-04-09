using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Data.Context;
using NHibernate;
using System.Data;

namespace Simple
{
    public static class DataContextSimplyExtensions
    {
        public static IDataContext EnterContext(this Simply simple)
        {
            return DataContextFactory.Do[simple.ConfigKey].EnterContext();
        }
        public static IDataContext GetContext(this Simply simple)
        {
            return DataContextFactory.Do[simple.ConfigKey].GetContext();
        }
        public static ISession GetSession(this Simply simple)
        {
            return DataContextFactory.Do[simple.ConfigKey].GetSession();
        }
        public static ITransaction BeginTransaction(this Simply simple)
        {
            return simple.GetSession().BeginTransaction();
        }
        public static ITransaction BeginTransaction(this Simply simple, IsolationLevel isolationLevel)
        {
            return simple.GetSession().BeginTransaction(isolationLevel);
        }
        public static ISession NewSession(this Simply simple)
        {
            return DataContextFactory.Do[simple.ConfigKey].NewSession();
        }

    }
}
