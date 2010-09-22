using System.Data;
using NHibernate;
using Simple.Data.Context;
using System.Collections.Generic;

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
        public static IEnumerable<IDataContext> GetContextList(this Simply simple)
        {
            return DataContextFactory.Do[simple.ConfigKey].GetContextList();
        }
        public static IDataContext GetContext(this Simply simple, bool throwException)
        {
            return DataContextFactory.Do[simple.ConfigKey].GetContext(throwException);
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
