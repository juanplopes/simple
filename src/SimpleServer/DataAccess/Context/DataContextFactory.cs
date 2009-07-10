using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;
using Simple.Server;
using Simple.Threading;

namespace Simple.DataAccess.Context
{
    public class DataContextFactory : AggregateFactory<DataContextFactory>, Simple.DataAccess.Context.IDataContextFactory
    {
        ThreadData _data = new ThreadData();
        object _myKey = new object();

        public IDataContext EnterContext()
        {
            IDataContext context = GetContext();
            if (context != null)
                throw new InvalidOperationException("There is another DataContext open. Use DataContext.NewContext instead");

            return new DataContext(() => SessionManager.OpenSession(ConfigKey), null, true);
        }

        public IDataContext GetContext()
        {
            IDataContext context = _data.Get<IDataContext>(_myKey);
            if (context != null && context.IsOpen) return context; 
            else return null;
        }
    }
}
