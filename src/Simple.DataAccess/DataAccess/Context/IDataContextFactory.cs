using System;
using NHibernate;
namespace Simple.DataAccess.Context
{
    public interface IDataContextFactory
    {
        IDataContext EnterContext();
        IDataContext GetContext();
        ISession GetSession();
        ISession NewSession();
    }
}
