using System;
using System.Collections.Generic;

using System.Text;
using NHibernate.Cfg;
using System.Xml;
using NHibernate;
using SimpleLibrary.Config;
using BasicLibrary.Threading;
using BasicLibrary.Logging;
using BasicLibrary.Cache;
using NHibernate.Mapping;
using NHibernate.Engine;
using System.IO;
using NHibernate.Mapping.Attributes;

namespace SimpleLibrary.DataAccess
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

        public static Configuration GetConfig()
        {
            return Factory.GetConfig();
        }

        public static Configuration GetConfig(string factoryName)
        {
            return Factory.GetConfig(factoryName);
        }

    }
}
