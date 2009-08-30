using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using NHibernate.Cfg;
using NHibernate;
using Simple.Patterns;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Cfg.Loquacious;
using System.Reflection;

namespace Simple.DataAccess
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
                        _sessionFactory = NHConfiguration.BuildSessionFactory();

                    //MemoryStream mem = new MemoryStream();
                    //new BinaryFormatter().Serialize(mem, _sessionFactory);
                    //mem.Seek(0, SeekOrigin.Begin);
                    //_sessionFactory = (ISessionFactory)new BinaryFormatter().Deserialize(mem);


                    return _sessionFactory;
                }
            }
        }

        private ValidatorEngine _validator = null;
        public virtual ValidatorEngine Validator
        {
            get
            {
                lock (_lock)
                {
                    if (_validator == null)
                        throw new AssertionFailure("Validator shouldn't be null. Might be a Simple.NET bug.");
                    return _validator;
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

                        _validator = new ValidatorEngine();
                        _validator.Configure();
                        ValidatorInitializer.Initialize(_configuration, _validator);

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
                _validator = null;
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
