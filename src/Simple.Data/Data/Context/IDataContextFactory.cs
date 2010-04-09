using System;
using NHibernate;
namespace Simple.Data.Context
{
    public interface IDataContextFactory
    {
        IDataContext EnterContext();
        IDataContext GetContext();
        ISession GetSession();
        ISession NewSession();
    }
}
