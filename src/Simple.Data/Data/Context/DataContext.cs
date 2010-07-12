using System;
using System.Collections.Generic;
using NHibernate;
using log4net;
using System.Reflection;

namespace Simple.Data.Context
{
    public class DataContext : IDataContext
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());

        #region IDataContext Members

        protected Func<ISession> _mainCreator = null;
        protected Func<ISession> _addCreator = null;
        protected bool _isMain = false;
        protected ISession _defaultSession = null;
        protected IList<ISession> _addSessions = new List<ISession>();
        protected bool _isOpen = false;

        internal DataContext(Func<ISession> mainSessionCreator,
            Func<ISession> additionalSessionCreator, bool isMain)
        {
            _mainCreator = mainSessionCreator;
            _addCreator = additionalSessionCreator;
            _isMain = isMain;
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
                if (_defaultSession == null)
                    _defaultSession = _mainCreator();

                return _defaultSession;
            }
        }

        public NHibernate.ISession NewSession()
        {
            lock (this)
            {
                ISession ret = null;
                _addSessions.Add(ret = _addCreator());
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
            if (_defaultSession != null)
            {
                if (!_defaultSession.IsOpen)
                    throw new InvalidOperationException("You shoudn't close the main session. Correct your code right now");

                FlushSession(_defaultSession);
                _defaultSession.Close();
            }
        }

        protected void CloseAdditionalSessions()
        {
            foreach (ISession session in _addSessions)
            {
                FlushSession(session);
                session.Close();
            }
        }

        private void FlushSession(ISession session)
        {
            try
            {
                session.Flush();
            }
            catch (AssertionFailure)
            {
                logger.Warn("Exception ocurred. Skipping AssertionFailure");
            }
        }

        public void Exit()
        {
            this.Dispose();
        }
    }
}
