using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Logging;
using Simple.Config;
using Simple.ConfigSource;
using Simple.Cfg;

namespace Simple.Tests.SimpleLib
{
    [TestFixture, Category("Configuration")]
    public class LoggerFactoryFixture
    {
        [SetUp]
        public void Setup()
        {
            SourcesManager.ClearSource<Log4netConfig>();
        }

        [Test]
        public void NullLoggerTests()
        {
            SimpleLogger.Get(this).Debug("Teste");
        }
    }
}
