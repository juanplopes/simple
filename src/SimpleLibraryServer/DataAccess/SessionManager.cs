using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using System.Xml;
using NHibernate;
using SimpleLibrary.Config;
using BasicLibrary.Threading;
using BasicLibrary.Logging;

namespace SimpleLibrary.DataAccess
{
    public class SessionManager
    {
        public static Configuration DefaultConfig { get; set; }
        public static ISessionFactory DefaultSessionFactory { get; set; }

        public static Dictionary<string, Configuration> Configs { get; set; }
        public static Dictionary<string, ISessionFactory> SessionFactories { get; set; }

        private static ThreadData<SessionManager> MyData { get; set; }
        private static string ISESSION_KEY = typeof(ISession).GUID.ToString();

        public static ISession GetThreadSession(string factoryName)
        {
            return GetThreadSession(factoryName, true);
        }

        public static ISession GetThreadSession(string factoryName, bool createIfDoesNotExist)
        {
            lock (SessionFactories)
                lock (MyData)
                {
                    string realName = factoryName ?? "";

                    ISession session = (ISession)MyData[ISESSION_KEY + realName, 0];
                    if (session == null || !((ISession)session).IsOpen)
                    {
                        if (!createIfDoesNotExist) return null;

                        session = GetSessionFactory(factoryName).OpenSession();
                        MyData[ISESSION_KEY + realName, 0] = session;
                    }
                    return session;
                }
        }

        public static void ClearThreadSessions()
        {
            lock (SessionFactories)
            {
                foreach (ISession session in GetAllSessions())
                {
                    if (session.IsOpen && session.IsConnected) session.Clear();
                }
            }
        }

        public static void ReleaseThreadSessions()
        {
            lock (SessionFactories)
            {
                foreach (ISession session in GetAllSessions())
                {
                    if (session.IsOpen) session.Close();
                }
            }
        }

        public static IEnumerable<ISession> GetAllSessions()
        {
            ISession session = GetThreadSession(null, false);
            if (session != null) yield return session;
            foreach (string factoryName in SessionFactories.Keys)
            {
                session = GetThreadSession(factoryName, false);
                if (session != null)
                    yield return session;
            }
        }

        public static ISessionFactory GetSessionFactory(string factoryName)
        {
            lock (SessionFactories)
            {
                if (factoryName == null)
                {
                    return DefaultSessionFactory;
                }
                else
                {
                    try
                    {
                        return SessionFactories[factoryName];
                    }
                    catch (KeyNotFoundException) { throw new InvalidOperationException("Invalid factory name: " + factoryName); }
                }
            }
        }

        protected static void InitializeSessionFactories()
        {
            SimpleLibraryConfig libConfig = SimpleLibraryConfig.Get();

            DefaultConfig = new Configuration();
            DefaultConfig.Configure(libConfig.DataConfig.DefaultSessionFactory.ConfigFile);
            DefaultSessionFactory = DefaultConfig.BuildSessionFactory();

            foreach (SessionFactoryElement factoryConfig in libConfig.DataConfig.SessionFactories)
            {
                Configuration config = new Configuration();
                config.Configure(factoryConfig.ConfigFile);
                SessionFactories[factoryConfig.Name] = config.BuildSessionFactory();
                Configs[factoryConfig.Name] = config;
            }
        }

        static SessionManager()
        {
            SessionFactories = new Dictionary<string, ISessionFactory>();
            lock (SessionFactories)
            {
                MyData = new ThreadData<SessionManager>();
                Configs = new Dictionary<string, Configuration>();
                InitializeSessionFactories();
            }
        }

        public static ISession GetSession()
        {
            return GetSession(null);
        }

        public static ISession GetSession(string factoryName)
        {
            return GetSession(factoryName, false);
        }

        public static ISession GetSession(bool forceNewSession)
        {
            return GetSession(null, forceNewSession);
        }

        public static ISession GetSession(string factoryName, bool forceNewSession)
        {
            if (forceNewSession)
            {
                if (factoryName == null) return DefaultSessionFactory.OpenSession();
                else return SessionFactories[factoryName].OpenSession();
            }
            else
            {
                return GetThreadSession(factoryName);
            }
        }
    }
}
