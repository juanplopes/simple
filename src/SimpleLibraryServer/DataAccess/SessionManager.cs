using System;
using System.Collections.Generic;

using System.Text;
using NHibernate.Cfg;
using System.Xml;
using NHibernate;
using Simple.Config;
using Simple.Threading;
using Simple.Logging;
using Simple.Cache;
using NHibernate.Mapping;
using NHibernate.Engine;
using System.IO;
using NHibernate.Mapping.Attributes;

namespace Simple.DataAccess
{
    public class SessionManager
    {
        protected static object locker = new object();

        protected static ThreadData<SessionManager> MyData = new ThreadData<SessionManager>();
        protected const string LockerId = "Locker";

        public static Guid? LockThreadSessions()
        {
            lock (locker)
            {
                Guid? identifier = (Guid?)MyData[LockerId, 0];
                if (identifier == null)
                {
                    return (Guid?)(MyData[LockerId, 0] = Guid.NewGuid());
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool ReleaseThreadSessions(Guid? identifier)
        {
            lock (locker)
            {
                Guid? identifier2 = (Guid?)MyData[LockerId, 0];
                if (identifier2 != null && identifier != identifier2) return false;
                ForceReleaseThreadSessions();
                MyData[LockerId, 0] = null;
                return true;
            }
        }

        protected static MultiSessionFactory _factory;
        protected static MultiSessionFactory Factory
        {
            get
            {
                lock (locker)
                {
                    if (_factory == null)
                    {
                        var config = SimpleLibraryConfig.Get();
                        _factory = new MultiSessionFactory(config.DataConfig, config.Business);
                    }
                    return _factory;
                }
            }
        }
        public static bool IsInitialized
        {
            get
            {
                lock (locker)
                    return _factory != null;
            }
        }

        public static ITransaction BeginTransaction()
        {
            return GetSession().BeginTransaction();
        }

        public static ITransaction BeginTransaction(bool forceNewSession)
        {
            return GetSession(forceNewSession).BeginTransaction();
        }

        public static ITransaction BeginTransaction(string factoryName)
        {
            return GetSession(factoryName).BeginTransaction();
        }

        public static ITransaction BeginTransaction(string factoryName, bool forceNewSession)
        {
            return GetSession(factoryName, forceNewSession).BeginTransaction();
        }

        public static ISession GetSession()
        {
            return Factory.GetSession();
        }

        public static ISession GetSession(bool forceNewSession)
        {
            return Factory.GetSession(forceNewSession);
        }

        public static ISession GetSession(string factoryName)
        {
            return Factory.GetSession(factoryName);
        }

        public static ISession GetSession(string factoryName, bool forceNewSession)
        {
            return Factory.GetSession(factoryName, forceNewSession);
        }

        public static void ForceReleaseThreadSessions()
        {
            Factory.ReleaseThreadSessions();
        }

        public static NHibernate.Cfg.Configuration GetConfig()
        {
            return Factory.GetConfig();
        }

        public static NHibernate.Cfg.Configuration GetConfig(string factoryName)
        {
            return Factory.GetConfig(factoryName);
        }

    }
}
