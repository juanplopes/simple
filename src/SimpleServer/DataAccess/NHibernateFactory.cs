using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using NHibernate.Cfg;
using NHibernate;
using Simple.Patterns;

namespace Simple.DataAccess
{
    public class NHibernateFactory : Factory<NHConfigurator>, INHibernateFactory
    {
        protected NHConfigurator ConfigHooks { get; set; }
        public NHConfigurator MappingHooks { get; protected set; }

        public NHibernateFactory()
        {
            ConfigHooks = new NHConfigurator();
            MappingHooks = new NHConfigurator();
        }

        private ISessionFactory _sessionFactory = null;
        public virtual ISessionFactory SessionFactory
        {
            get
            {
                lock (this)
                {
                    if (_sessionFactory == null)
                        _sessionFactory = NHConfiguration.BuildSessionFactory();

                    return _sessionFactory;
                }
            }
        }

        private Configuration _configuration = null;
        public virtual Configuration NHConfiguration
        {
            get
            {
                lock (this)
                {
                    if (_configuration == null)
                        _configuration = MappingHooks.Invoke(
                            ConfigHooks.Invoke(new Configuration()));

                    return _configuration;
                }
            }
        }

        public virtual ISession OpenNewSession()
        {
            return SessionFactory.OpenSession();
        }

        protected override void OnConfig(NHConfigurator config)
        {
            lock (this)
            {
                ConfigHooks = config;
                _configuration = null;
                _sessionFactory = null;
            }
        }

        protected override void OnClearConfig()
        {
            lock (this)
            {
                ConfigHooks = new NHConfigurator();
                _configuration = null;
                _sessionFactory = null;
            }
        }




    }
}
