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

        internal DataContext(Func<ISession> sessionCreator, ISession defaultSession)
        {
            _creator = sessionCreator;
            _isMain = defaultSession == null;
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
                _isOpen = false;

                if (_isMain)
                    CloseMainSession();

                CloseAdditionalSessions();
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

        public void Exit()
        {
            this.Dispose();
        }
    }
}
