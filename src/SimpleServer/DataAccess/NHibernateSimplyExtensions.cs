using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.DataAccess;
using Simple.ConfigSource;
using NHibernate;
using NHibernate.Cfg;

namespace Simple
{
    public static class NHibernateSimplyExtensions
    {
        private static FactoryManager<NHibernateFactory, NHConfigurator> factories =
        new FactoryManager<NHibernateFactory, NHConfigurator>(
            () => new NHibernateFactory());

        private static NHibernateFactory Factory(this Simply simply)
        {
            return factories[simply.ConfigKey];
        }

        /// <summary>
        /// Gets a new session, without link to current DataContext. You must dispose it at
        /// end of use. Always prefer to use NewSession, instead.
        /// </summary>
        /// <param name="simply"></param>
        /// <returns></returns>
        public static ISession OpenSession(this Simply simply)
        {
            return Factory(simply).OpenNewSession();
        }

        public static Configuration GetNHibernateConfig(this Simply simply)
        {
            return Factory(simply).NHConfiguration;
        }
        public static ISessionFactory GetSessionFactory(this Simply simply)
        {
            return Factory(simply).SessionFactory;
        }

    }
}
