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

        public static ITransaction BeginTransaction()
        {
            return GetSession().BeginTransaction();
        }

        public static ITransaction BeginTransaction(bool forceNewSession)
        {
            return GetSession(forceNewSession).BeginTransaction();
        }

        public static ITransaction BeginTransaction(object key)
        {
            return GetSession(key).BeginTransaction();
        }

        public static ITransaction BeginTransaction(object key, bool forceNewSession)
        {
            return GetSession(key, forceNewSession).BeginTransaction();
        }

        public static ISession GetSession()
        {
            return Factory().GetSession();
        }

        public static ISession GetSession(bool forceNewSession)
        {
            return Factory().GetSession();
        }

        public static ISession GetSession(object key)
        {
            return Factory(key).GetSession();
        }

        public static ISession GetSession(object key, bool forceNewSession)
        {
            return Factory(key).GetSession();
        }

        public static NHibernate.Cfg.Configuration GetConfig()
        {
            return Factory().Configuration;
        }

        public static NHibernate.Cfg.Configuration GetConfig(object key)
        {
            return Factory(key).Configuration;
        }
    }
}
