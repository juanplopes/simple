using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Simple.DataAccess.Context
{
    public class DataContext : IDataContext
    {
        #region IDataContext Members

        protected Func<ISession> _creator = null;
        protected bool _isMain = false;
        protected ISession _defaultSession = null;
        protected IList<ISession> _addSessions = new List<ISession>();
        protected bool _isOpen = false;

        internal DataContext(Func<ISession> sessionCreator, ISession defaultSession, bool mainContext)
        {
            _creator = sessionCreator;
            _isMain = mainContext;
            _defaultSession = defaultSession ?? sessionCreator();
            _isOpen = true;
        }

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
        }

        public NHibernate.ISession Session
        {
            get
            {
                return _defaultSession;
            }
        }

        public NHibernate.ISession NewSession()
        {
            lock (this)
            {
                ISession ret = null;
                _addSessions.Add(ret = _creator());
                return ret;
            }
        }

        #endregion

        public void Dispose()
        {
            if (_isOpen)
            {
                if (_isMain)
                    CloseMainSession();

                CloseAdditionalSessions();

                _isOpen = false;
            }
        }

        protected void CloseMainSession()
        {
            if (!_defaultSession.IsOpen)
                throw new InvalidOperationException("You shoudn't close the main session. Correct your code right now");

            _defaultSession.Clear();
            _defaultSession.Close();
        }

        protected void CloseAdditionalSessions()
        {
            foreach (ISession session in _addSessions)
            {
                session.Clear();
                session.Close();
            }
        }

        public IDataContext NewContext()
        {
            return new DataContext(_creator, _defaultSession, false);
        }

        public void Exit()
        {
            this.Dispose();
        }
    }
}
