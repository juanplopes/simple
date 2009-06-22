using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Cfg;
using Simple.ConfigSource;
using NHibernate.Cfg;
using NHibernate;

namespace Simple.DataAccess
{
    public class NHibernateFactory : Factory<Configuration>
    {
        protected Configuration NHConfig { get; set; }
        private ISessionFactory _sessionFactory = null;

        public ConfigHookDelegate<Configuration> MappingHook { get; set; }

        protected ISessionFactory SessionFactory
        {
            get
            {
                lock (this)
                {
                    if (_sessionFactory == null)
                        _sessionFactory = NHConfig.BuildSessionFactory();

                    return _sessionFactory;
                }
            }
        }

        protected override void Config(Configuration config)
        {
            NHConfig = config;
        }

        public override void InitDefault()
        {
            NHConfig = new Configuration();
        }


    }
}
