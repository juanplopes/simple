﻿using System;
using System.Collections.Generic;
using NHibernate;
using log4net;
using System.Reflection;

namespace Simple.Data.Context
{
    public class DataContext : IDataContext
    {
        object _lock = new object();
        static ILog log = Simply.Do.Log(MethodBase.GetCurrentMethod());
        Guid guid = Guid.NewGuid();

        public IDataContext Parent { get; protected set; }
        public IDataContext Child { get; protected set; }
        protected bool HasChild { get { return Child != null && Child.IsOpen; } }

        protected ISession session = null;
        public NHibernate.ISession Session
        {
            get
            {
                if (session == null)
                    session = createMainSession();

                return session;
            }
        }
        public bool IsOpen { get; protected set; }
        public bool IsConnected { get { return IsMain ? (session != null) : Parent.IsConnected; } }

        protected bool IsMain { get { return Parent == null; } }
        protected ICollection<ISession> AdditionalSessions { get; set; }

        protected Func<ISession> createMainSession = null;
        protected Func<ISession> createAdditionalSession = null;

        public DataContext(Func<ISession> newSession) : this(null, newSession) { }

        protected DataContext(IDataContext parent, Func<ISession> newSession)
        {
            Parent = parent;

            log.InfoFormat("Opening data context. Is main: {0}. Guid: {1}", IsMain, guid);

            createMainSession = IsMain ? newSession : () => parent.Session;
            createAdditionalSession = newSession;
            IsOpen = true;
            AdditionalSessions = new LinkedList<ISession>();
        }

        public ISession NewSession()
        {
            lock (_lock)
            {
                ISession ret = createAdditionalSession();
                AdditionalSessions.Add(ret);
                return ret;
            }
        }

        public IDataContext NewContext()
        {
            lock (_lock)
            {
                if (!IsOpen) throw new InvalidOperationException("Cannot create child context on closed object.");
                if (HasChild) throw new InvalidOperationException("Each context can only have one open child context at a moment");

                return Child = new DataContext(this, createAdditionalSession);
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (!IsOpen) return;

                log.InfoFormat("Closing data context. Guid: {0}", guid);

                IsOpen = false;
                
                if (HasChild)
                    Child.Dispose();

                CloseAdditionalSessions();

                if (IsMain)
                    CloseMainSession();
            }
        }

        protected void CloseMainSession()
        {
            lock (_lock)
            {
                if (session != null)
                {
                    if (!session.IsOpen)
                        throw new InvalidOperationException("You shoudn't close the main session. Correct your code right now");
                    FlushSession(session);
                    session.Close();
                }
            }
        }

        protected void CloseAdditionalSessions()
        {
            lock (_lock)
            {
                foreach (ISession session in AdditionalSessions)
                {
                    FlushSession(session);
                    session.Close();
                }
            }
        }

        private void FlushSession(ISession session)
        {
            try
            {
                session.Flush();
            }
            catch (AssertionFailure e)
            {
                log.Warn("Exception ocurred. Skipping AssertionFailure", e);
            }
            catch (Exception e)
            {
                log.Warn(e.Message, e);
            }
        }

        public void Exit()
        {
            this.Dispose();
        }
    }
}
