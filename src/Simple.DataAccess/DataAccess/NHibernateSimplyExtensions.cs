using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.DataAccess;
using Simple.Config;
using NH = NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using System.Reflection;
using System.Linq.Expressions;
using Simple.Expressions;
using NHibernate;
using FluentNHibernate;

namespace Simple
{
    public static class NHibernateSimplyExtensions
    {
        #region Simply
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
        public static string GetConnectionString(this Simply simply)
        {
            return GetNHibernateConfig(simply).GetProperty(NH.Cfg.Environment.ConnectionString);
        }
        public static ISessionFactory GetSessionFactory(this Simply simply)
        {
            return Factory(simply).SessionFactory;
        }
        

        #endregion
        #region Configure

        public static IConfiguratorInterface<NHibernateConfig, SimplyConfigure> NHibernate(this SimplyConfigure config)
        {
            return new ConfiguratorInterface<NHibernateConfig, SimplyConfigure>(x => NHibernate(config, x));
        }

        public static SimplyConfigure NHibernteFluently(this SimplyConfigure config, Func<FluentConfiguration, FluentConfiguration> func)
        {
            return NHibernate(config, x => func(Fluently.Configure(x)).BuildConfiguration());
        }

        public static SimplyConfigure NHibernate(this SimplyConfigure config, Func<Configuration, Configuration> configurator)
        {
            NHConfigurator hooks = new NHConfigurator();
            hooks.Add(configurator);

            SourceManager.Do.Register(config.ConfigKey,
                new DirectConfigSource<NHConfigurator>().Load(hooks));

            return config;
        }

        public static SimplyConfigure NHibernate(this SimplyConfigure config, IConfigSource<NHibernateConfig> source)
        {
            SourceManager.Do.Register(config.ConfigKey, new NHibernateConfigSource().Load(source));
            return config;
        }

        public static SimplyConfigure Mapping(this SimplyConfigure config, Func<Configuration, Configuration> func)
        {
            SourceManager.Do.Get<NHConfigurator>(config.ConfigKey).AddTransform(x =>
            {
                x.Add(func);
                return x;
            });
            return config;
        }

        public static SimplyConfigure Mapping(this SimplyConfigure config, Type type)
        {
            return Mapping(config, x =>
            {
                return Fluently.Configure(x).Mappings(m =>
                {
                    m.FluentMappings.Add(type);
                }).BuildConfiguration();
            });
        }
        public static SimplyConfigure Mapping<T>(this SimplyConfigure config)
            where T : IMappingProvider
        {
            return Mapping(config, typeof(T));
        }

        public static SimplyConfigure MappingFromAssembly(this SimplyConfigure config, Assembly asm)
        {
            return Mapping(config, x =>
            {
                return Fluently.Configure(x).Mappings(m =>
                {
                    m.FluentMappings.AddFromAssembly(asm);
                }).BuildConfiguration();
            });
        }

        public static SimplyConfigure MappingFromAssemblyOf<T>(this SimplyConfigure config)
        {
            return MappingFromAssembly(config, typeof(T).Assembly);
        }
        #endregion

        public static SimplyRelease NHibernate(this SimplyRelease config)
        {
            SourceManager.Do.Remove<NHConfigurator>(config.ConfigKey);
            return config;
        }
    }
}
