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

        public static void ReleaseThreadSessions()
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
