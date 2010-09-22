using System;
using System.Collections.Generic;
using NHibernate;
using Simple.Common;
using Simple.Config;

namespace Simple.Data.Context
{
    public class DataContextFactory : AggregateFactory<DataContextFactory>, IDataContextFactory
    {
        ThreadData _data = new ThreadData();
        object _myKey = new object();

        public IDataContext EnterContext()
        {
            var context = GetContext(false);

            if (context == null)
                context = new DataContext(() => Simply.Do[ConfigKey].OpenSession());
            else
                context = context.NewContext();

            SetContext(context);

            return context;
        }

        public IEnumerable<IDataContext> GetContextList()
        {
            var last = GetContext(false);

            while (last != null)
            {
                yield return last;
                last = last.Parent;
            }
        }

        public IDataContext GetContext()
        {
            return GetContext(true);
        }

        protected void SetContext(IDataContext context)
        {
            _data.Set(_myKey, context);
        }

        public IDataContext GetContext(bool throwException)
        {
            var context = _data.Get<IDataContext>(_myKey);

            while (context != null && !context.IsOpen)
                context = context.Parent;

            while (context != null && context.Child != null && context.Child.IsOpen)
                context = context.Child;

            if (context != null) return context;
            else if (throwException) throw new InvalidOperationException("There is no open DataContext");
            else return null;
        }



        #region IDataContextFactory Members

        public NHibernate.ISession GetSession()
        {
            return GetContext().Session;
        }

        public NHibernate.ISession NewSession()
        {
            return GetContext(true).NewSession();
        }

        #endregion
    }
}
