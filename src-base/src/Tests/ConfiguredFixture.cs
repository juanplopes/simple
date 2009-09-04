using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Sample.Project.Environment.Test;
using Simple.Patterns;
using Sample.Project.Migrations.Extensions;

namespace Sample.Project.Tests
{
    public class ConfiguredFixture
    {
        [TestFixtureSetUp]
        public virtual void FixtureSetup()
        {
            ConfigurationEnsurer.Do.Ensure();
        }
    }

    public class ConfigurationEnsurer : Singleton<ConfigurationEnsurer>
    {
        public ConfigurationEnsurer()
        {
            Default.ConfigureServer();
            Simply.Do.InitServer(typeof(ServerStarter).Assembly, false);
        }

        public void Ensure() { }

        public void ReMigrate()
        {
            var cfg = Simply.Do.GetNHibernateConfig();
            var cs = cfg.GetProperty("connection.connection_string");
            var mig = new Migrator.Migrator("SqlServer", cs, typeof(MigratorExtensions).Assembly, false);
            mig.MigrateTo(1);
            mig.MigrateToLastVersion();

        }
    }
}
