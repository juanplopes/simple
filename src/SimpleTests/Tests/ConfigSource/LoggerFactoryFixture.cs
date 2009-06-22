using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Logging;
using Simple.Config;
using Simple.ConfigSource;
using Simple.Cfg;

namespace Simple.Tests.ConfigSource
{
    [TestFixture, Category("Configuration")]
    public class LoggerFactoryFixture
    {
        [SetUp]
        public void Setup()
        {
            SourcesManager.RemoveSource<Log4netConfig>();
        }

        [Test]
        public void NullLoggerTests()
        {
            SimpleLogger.Get(this).Debug("Teste");
        }
    }
}
