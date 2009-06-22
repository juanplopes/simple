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
    public class NHibernateFactory : Factory<Configuration>
    {
        protected Configuration NHConfig { get; set; }
        private ISessionFactory _sessionFactory = null;

        public TransformationList<Configuration> MappingHooks { get; set; }

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

        protected ISessionFactory BuildSessionFactory()
        {
            return null;
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
