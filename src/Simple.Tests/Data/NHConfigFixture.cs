using FluentNHibernate.Cfg.Db;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Config;
using Simple.Data;
using Simple.Tests.Resources;
using FluentNHibernate.Cfg;

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

            factory.NHConfiguration.GetProperty("dialect").StartsWith("NHibernate.Dialect.SQLiteDialect").Should().Be.True();
        }

        [Test]
        public void TestInjectConfiguration()
        {
            var cfg = Fluently.Configure().
                Database(SQLiteConfiguration.Standard.UsingFile("myfilemyfilemyfile")).BuildConfiguration();

            Simply.Do[this].Release.NHibernate();
            Simply.Do[this].GetNHibernateConfig().GetProperty("dialect").Should().Be(null);
            Simply.Do[this].SetNHibernateConfig(cfg);
            StringAssert.StartsWith("NHibernate.Dialect.SQLiteDialect", Simply.Do[this].GetNHibernateConfig().GetProperty("dialect"));

            Simply.Do[this].GetConnectionString().Contains("myfilemyfilemyfile").Should().Be.True();
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

            factory.NHConfiguration.ClassMappings.Count.Should().Be(1);
        }
        [Test]
        public void TestMapEntityAssembly()
        {
            Simply.Do[this].Configure.NHibernteFluently(x =>
               x.Database(SQLiteConfiguration.Standard.UsingFile("Northwind.sl3")))
            .MappingFromAssemblyOf<Category.Map>();

            var config = Simply.Do[this].GetNHibernateConfig();

            config.ClassMappings.Count.Should().Be(8);
        }
    }
}
