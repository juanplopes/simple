using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Logging;
using Simple.Config;
using Simple.ConfigSource;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class LoggerFactoryFixture
    {
        [SetUp]
        public void Setup()
        {
            SourcesManager<LoggerConfig>.RemoveSource();
        }

        [Test]
        public void NullLoggerTests()
        {
            SimpleLogger.Get(this).Debug("Teste");
        }
    }
}
