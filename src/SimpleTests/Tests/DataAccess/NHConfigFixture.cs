using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate.Tool.hbm2ddl;
using Simple.ConfigSource;
using Simple.DataAccess;
using Simple.Services.Remoting;
using Simple.Tests.Service;
using FluentNHibernate.Cfg;
using NHibernate;
using Simple.Tests.SampleServer;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class NHConfigFixture
    {
        [Test]
        public void TestLoadDialect()
        {
            SourceManager.Do.Remove<NHConfigurator>();
            SourceManager.Do.Register(this, new NHibernateConfigSource().Load(
                new XmlFileConfigSource<NHibernateConfig>().Load(NHConfigurations.NHConfig1)));

            var factories = new FactoryManager<NHibernateFactory, NHConfigurator>();
            var factory = factories[this];

            Assert.AreEqual("NHibernate.Dialect.SQLiteDialect", factory.NHConfiguration.GetProperty("dialect"));
        }

        [Test]
        public void TestMapEntities()
        {
            Simply.Get(this).Configure
                .NHibernate().FromXml(NHConfigurations.NHConfig1)
                .Mapping<Category.Map>();
                
            var factories = new FactoryManager<NHibernateFactory, NHConfigurator>();
            var factory = factories[this];

            Assert.AreEqual(1, factory.NHConfiguration.ClassMappings.Count);
        }
        [Test]
        public void TestMapEntityAssembly()
        {
            Simply.Get(this).Configure
                 .NHibernate().FromXml(NHConfigurations.NHConfig1)
                 .MappingFromAssemblyOf<Category.Map>();

            var config = Simply.Get(this).GetNHibernateConfig();

            Assert.AreEqual(4, config.ClassMappings.Count);
        }
    }
}
