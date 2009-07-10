using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using Simple.Patterns;
using Simple.ConfigSource;
using NHibernate;

namespace Simple.DataAccess
{
    public class SessionManager
    {
        private static FactoryManager<NHibernateFactory, NHConfigurator> factories =
            new FactoryManager<NHibernateFactory, NHConfigurator>(
                () => new NHibernateFactory());

        private static NHibernateFactory Factory()
        {
            return factories.SafeGet();
        }

        private static NHibernateFactory Factory(object key)
        {
            return factories[key];
        }

        public static ISession OpenSession()
        {
            return Factory().OpenNHSession();
        }

        public static ISession OpenSession(object key)
        {
            return Factory(key).OpenNHSession();
        }

        public static NHibernate.Cfg.Configuration GetConfig()
        {
            return Factory().NHConfiguration;
        }

        public static NHibernate.Cfg.Configuration GetConfig(object key)
        {
            return Factory(key).NHConfiguration;
        }
    }
}
