using System;
using System.Collections.Generic;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;
using Simple.Threading;

namespace Simple.DataAccess.Context
{
    public class DataContextFactory : AggregateFactory<DataContextFactory>, IDataContextFactory
    {
        ThreadData _data = new ThreadData();
        object _myKey = new object();

        public IDataContext EnterContext()
        {
            var contextList = GetContextList();
            var curContext = GetContext(false);

            var context = new DataContext(() => SessionManager.OpenSession(ConfigKey),
                curContext != null ? curContext.Session : null);
            contextList.AddLast(context);

            return context;
        }

        public IDataContext GetContext()
        {
            return GetContext(true);
        }

        protected IDataContext GetContext(bool throwException)
        {
            var contextList = GetContextList();

            IDataContext context = null;
            while (contextList.Count > 0 && (context == null || !context.IsOpen))
            {
                context = contextList.Last.Value;
                if (!context.IsOpen) contextList.RemoveLast();
            }

            if (context != null && context.IsOpen) return context;
            else if (throwException) throw new InvalidOperationException("There is no open DataContext");
            else return null;
        }

        protected LinkedList<IDataContext> GetContextList()
        {
            var col = _data.Get<LinkedList<IDataContext>>(_myKey);
            if (col == null)
            {
                col = new LinkedList<IDataContext>();
                _data.Set(_myKey, col);
            }
            return col;
        }

        #region IDataContextFactory Members


        public NHibernate.ISession GetSession()
        {
            return GetContext().Session;
        }

        public NHibernate.ISession NewSession()
        {
            var context = GetContext(false);
            if (context == null)
            {
                return SessionManager.OpenSession(ConfigKey);
            }
            else
            {
                return context.Session;
            }
        }

        #endregion
    }
}
