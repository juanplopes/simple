using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate.Tool.hbm2ddl;
using Simple.Config;
using Simple.Data;
using Simple.Services.Remoting;
using Simple.Tests.Services;
using FluentNHibernate.Cfg;
using NHibernate;
using Simple.Tests.Resources;
using FluentNHibernate.Cfg.Db;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class NHConfigFixture
    {
        [Test]
        public void TestLoadDialect()
        {
            Simply.Do[this].Configure.NHibernteFluently(x =>
               x.Database(SQLiteConfiguration.Standard.UsingFile("Northwind.sl3")));


            var factories = new FactoryManager<NHibernateFactory, NHConfigurator>();
            var factory = factories[this];

            Assert.IsTrue(factory.NHConfiguration.GetProperty("dialect").StartsWith("NHibernate.Dialect.SQLiteDialect"));
        }

        [Test]
        public void TestGetConnectionString()
        {
            Simply.Do[this].Configure.NHibernteFluently(x =>
               x.Database(SQLiteConfiguration.Standard.UsingFile("Northwind.sl3")));
            var cs = Simply.Do[this].GetConnectionString();

            StringAssert.Contains("Northwind.sl3", cs);

        }

        [Test]
        public void TestMapEntities()
        {
            Simply.Do[this].Configure.NHibernteFluently(x =>
                    x.Database(SQLiteConfiguration.Standard.UsingFile("Northwind.sl3")))
                .Mapping<Category.Map>();

            var factories = new FactoryManager<NHibernateFactory, NHConfigurator>();
            var factory = factories[this];

            Assert.AreEqual(1, factory.NHConfiguration.ClassMappings.Count);
        }
        [Test]
        public void TestMapEntityAssembly()
        {
            Simply.Do[this].Configure.NHibernteFluently(x =>
               x.Database(SQLiteConfiguration.Standard.UsingFile("Northwind.sl3")))
            .MappingFromAssemblyOf<Category.Map>();

            var config = Simply.Do[this].GetNHibernateConfig();

            Assert.AreEqual(8, config.ClassMappings.Count);
        }
    }
}
