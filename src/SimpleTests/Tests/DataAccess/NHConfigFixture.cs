using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;
using Simple.ConfigSource;
using Simple.DataAccess;
using Simple.Remoting;
using Simple.Tests.Service;

namespace Simple.Tests.DataAccess
{
    [TestClass]
    public class NHConfigFixture
    {
        [TestMethod]
        public void TestSchemaCreation()
        {
            NHibernateSimply.Do.Configure(this,
                XmlConfig.LoadXml<NHibernateConfig>(NHConfigurations.NHConfig1));

            SchemaExport exp = new SchemaExport(SessionManager.GetConfig(this));
            exp.Drop(true, true);
            exp.Create(true, true);
        }

        [TestMethod]
        public void TestLoadDialect()
        {
            SourceManager.Do.Clear<NHConfigurator>();
            SourceManager.Do.Register(this, new NHibernateConfigSource().Load(
                new XmlFileConfigSource<NHibernateConfig>().Load(NHConfigurations.NHConfig1)));

            var factories = new FactoryManager<NHibernateFactory, NHConfigurator>();
            var factory = factories[this];

            Assert.AreEqual("NHibernate.Dialect.SQLiteDialect", factory.Configuration.GetProperty("dialect"));


        }
    }
}
