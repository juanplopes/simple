using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Logging;
using Simple.ConfigSource;

namespace Simple.Tests.ConfigSource
{
    [TestFixture]
    public class LoggerFactoryFixture
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            SourceManager.Do.Remove<Log4netConfig>(this);
        }

        [Test]
        public void NullLoggerTests()
        {
            Simply.Do.Log(this).Debug("Teste");
        }
    }
}
