using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Simple.Data.Context
{
    public interface IDataContext : IDisposable
    {
        ISession Session { get; }
        ISession NewSession();
        
        bool IsOpen { get; }
        void Exit();
    }
}
