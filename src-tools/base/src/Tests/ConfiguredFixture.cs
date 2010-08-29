using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Simple.Patterns;
using Example.Project.Config;

namespace Example.Project.Tests
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
            new Configurator(Configurator.Test)
                .StartServer(typeof(ServerStarter).Assembly);
        }

        public void Ensure() { }
    }
}
