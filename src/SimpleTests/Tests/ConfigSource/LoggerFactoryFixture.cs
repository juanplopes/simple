using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Logging;
using Simple.Config;
using Simple.ConfigSource;

namespace Simple.Tests.ConfigSource
{
    [TestClass]
    public class LoggerFactoryFixture
    {
        [TestInitialize]
        public void Setup()
        {
            SourcesManager.RemoveSource<Log4netConfig>();
        }

        [TestMethod]
        public void NullLoggerTests()
        {
            SimpleLogger.Get(this).Debug("Teste");
        }
    }
}
