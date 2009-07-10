using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using System.Reflection;

namespace Simple.DataAccess
{
    public class NHibernateSimply : Singleton<NHibernateSimply>, ISimply<NHibernateConfig>
    {
        #region ISimply<NHibernateConfig> Members

        public void Configure(object key, IConfigSource<NHibernateConfig> source)
        {
            SourceManager.Do.Register(key, new NHibernateConfigSource().Load(source));
        }

        public void Map(object key, Func<Configuration, Configuration> func)
        {
            SourceManager.Do.Get<NHConfigurator>(key).AddTransform(x =>
                {
                    x.Add(func);
                    return x;
                });
        }

        public void Map(object key, Type type)
        {
            Map(key, x =>
            {
                return Fluently.Configure(x).Mappings(m =>
                {
                    m.FluentMappings.Add(type);
                }).BuildConfiguration();
            });
        }

        public void Map<T>(object key)
            where T : IClassMap
        {
            Map(key, typeof(T));
        }

        public void MapAssembly(object key, Assembly asm)
        {
            Map(key, x =>
            {
                return Fluently.Configure(x).Mappings(m =>
                {
                    m.FluentMappings.AddFromAssembly(asm);
                }).BuildConfiguration();
            });
        }

        public void MapAssemblyOf<T>(object key)
        {
            Map(key, x =>
            {
                return Fluently.Configure(x).Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<T>();
                }).BuildConfiguration();
            });
        }

        #endregion
    }
}
