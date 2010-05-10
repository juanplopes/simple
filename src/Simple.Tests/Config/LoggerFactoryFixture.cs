using NUnit.Framework;
using Simple.Config;
using Simple.Logging;

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
    }
}
