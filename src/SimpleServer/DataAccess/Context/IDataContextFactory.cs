using System;
namespace Simple.DataAccess.Context
{
    public interface IDataContextFactory
    {
        IDataContext EnterContext();
        IDataContext GetContext();
    }
}
