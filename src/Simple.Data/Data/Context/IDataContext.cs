using System;
using NHibernate;

namespace Simple.Data.Context
{
    public interface IDataContext : IDisposable
    {
        IDataContext Parent { get; }
        IDataContext Child { get; }

        ISession Session { get; }
        ISession NewSession();
        IDataContext NewContext();

        bool IsOpen { get; }
        void Exit();
    }
}
