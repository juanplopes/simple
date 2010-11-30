using NUnit.Framework;
using SharpTestsEx;
using Simple.Config;
using Simple.Logging;
using System;
using Simple.Patterns;
using System.IO;
using Simple.IO;

namespace Simple.Tests.Config
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

        [Test]
        public void CanGetAnyLoggerName()
        {
            Simply.Do.Log("teste").Logger.Name.Should().Be("teste");
        }

        [Test]
        public void CanGetAnyLoggerTypeGeneric()
        {
            Simply.Do.Log<LoggerFactoryFixture>().Logger.Name.Should().Be("Simple.Tests.Config.LoggerFactoryFixture");
        }

        [Test]
        public void CanGetAnyLoggerType()
        {
            Simply.Do.Log(typeof(LoggerFactoryFixture)).Logger.Name.Should().Be("Simple.Tests.Config.LoggerFactoryFixture");
        }
    }
}
