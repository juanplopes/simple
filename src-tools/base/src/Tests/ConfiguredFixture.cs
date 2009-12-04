using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Simple.Patterns;
using Sample.Project.SampleData;
using Sample.Project.Environment;
using Sample.Project.Migrations;

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
            new Default(Default.Test).StartServer(typeof(ServerStarter).Assembly, false);
        }

        public void Ensure() { }

        public void ReMigrate()
        {
            MigratorProgram.Migrate(Default.MigrationProvider, 1);
            MigratorProgram.Migrate(Default.MigrationProvider, null);

        }
    }
}
