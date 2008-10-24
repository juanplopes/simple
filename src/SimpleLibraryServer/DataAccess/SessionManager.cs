using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using System.Xml;
using NHibernate;
using SimpleLibrary.Config;
using BasicLibrary.Threading;

namespace SimpleLibrary.DataAccess
{
    public class SessionManager
    {
        public static Configuration Config { get; set; }
        public static ISessionFactory SessionFactory { get; set; }
        private static ThreadData<SessionManager> MyData { get; set; }

        private static string ISESSION_KEY = typeof(ISession).GUID.ToString();

        public static ISession ThreadSession
        {
            get
            {
                return (ISession)MyData[ISESSION_KEY, 0];
            }
            set
            {
                MyData[ISESSION_KEY, 0] = value;
            }
        }

        public static bool HasThreadSession
        {
            get
            {
                return ThreadSession != null;
            }
        }


        static SessionManager()
        {
            SimpleLibraryConfig libConfig = SimpleLibraryConfig.Get();

            Config = new Configuration();
            Config.Configure(libConfig.DataConfig.NHibernateConfigFile);
            Config.SetInterceptor(new GenericInterceptor());
            SessionFactory = Config.BuildSessionFactory();

            MyData = new ThreadData<SessionManager>();
        }

        public static void HeartBeat()
        {
            GetSession();
        }

        public static void CreateThreadSession()
        {
            ThreadSession = SessionFactory.OpenSession();
        }
        public static ISession GetSession()
        {
            if (HasThreadSession)
                return ThreadSession;
            else
                return SessionFactory.OpenSession();
        }
    }
}
