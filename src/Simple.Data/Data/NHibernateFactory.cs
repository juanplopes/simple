using NHibernate;
using NHibernate.Cfg;
using Simple.Config;

namespace Simple.Data
{
    public class NHibernateFactory : Factory<NHConfigurator>, INHibernateFactory
    {
        protected NHConfigurator ConfigHooks { get; set; }
        public NHConfigurator MappingHooks { get; protected set; }
        protected object _lock = new object();

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
                lock (_lock)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = NHConfiguration.BuildSessionFactory();
                    }

                    return _sessionFactory;
                }
            }
        }

        private Configuration _configuration = null;
        public virtual Configuration NHConfiguration
        {
            get
            {
                lock (_lock)
                {
                    if (_configuration == null)
                    {
                        _configuration = MappingHooks.Invoke(
                            ConfigHooks.Invoke(new Configuration()));
                    }
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
            lock (_lock)
            {
                ConfigHooks = config;
                _configuration = null;
                _sessionFactory = null;
            }
        }

        protected override void OnClearConfig()
        {
            lock (_lock)
            {
                ConfigHooks = new NHConfigurator();
                _configuration = null;
                _sessionFactory = null;
            }
        }




    }
}
