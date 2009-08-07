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
using Simple.Tests.Contracts;
using NHibernate;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class NHConfigFixture
    {
        [Test]
        public void TestSchemaCreation()
        {
            Simply.Do.Configure.NHibernate().FromXml(NHConfigurations.NHConfig1);


            SchemaExport exp = new SchemaExport(Simply.Get(this).GetNHibernateConfig());
            exp.Drop(true, true);
            exp.Create(true, true);
        }

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
            Simply.Do.Configure
                .NHibernate().FromXml(NHConfigurations.NHConfig1)
                .Mapping<Empresa.Map>()
                .Mapping<Funcionario.Map>();
                
            var factories = new FactoryManager<NHibernateFactory, NHConfigurator>();
            var factory = factories[this];

            Assert.AreEqual(2, factory.NHConfiguration.ClassMappings.Count);
        }
        [Test]
        public void TestMapEntityAssembly()
        {
            Simply.Do.Configure
                 .NHibernate().FromXml(NHConfigurations.NHConfig1)
                 .MappingFromAssemblyOf<Empresa.Map>();

            var config = Simply.Get(this).GetNHibernateConfig();

            Assert.AreEqual(4, config.ClassMappings.Count);
        }
    }
}
