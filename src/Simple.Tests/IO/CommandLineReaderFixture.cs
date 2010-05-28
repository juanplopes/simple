using System;
using NUnit.Framework;
using Simple.IO;

namespace Simple.Tests.IO
{
    public class CommandLineReaderFixture
    {
        [Test]
        public void CanReadSomeIntValues()
        {
            var reader = new CommandLineReader("/a:1", "-b:2", "-c=3");
            Assert.AreEqual(1, reader.Get("a", 0));
            Assert.AreEqual(2, reader.Get("b", 0));
            Assert.AreEqual(3, reader.Get("c", 0));
            Assert.AreEqual(0, reader.Get("d", 0));
        }

        [Test]
        public void CanReadSomeNullableIntValues()
        {
            var reader = new CommandLineReader("/a:1", "-b:2", "--c", "3");
            Assert.AreEqual(1, reader.Get<int?>("a"));
            Assert.AreEqual(2, reader.Get<int?>("b"));
            Assert.AreEqual(3, reader.Get<int?>("c"));
            Assert.AreEqual(null, reader.Get<int?>("d"));
        }

        [Test]
        public void CanReadSomeDateTimeValues()
        {
            var reader = new CommandLineReader("/a:11/11/2000");
            Assert.AreEqual(new DateTime(2000, 11, 11), reader.Get("a", DateTime.Now));
        }

        [Test]
        public void CanReadInvalidFormatValues()
        {
            var reader = new CommandLineReader("/a:asdasd");
            Assert.AreEqual(new DateTime(2000, 11, 11), reader.Get("a", new DateTime(2000, 11, 11)));
        }

        [Test]
        public void CanReadEnumValues()
        {
            var reader = new CommandLineReader("/a:machine");
            Assert.AreEqual(EnvironmentVariableTarget.Machine, reader.Get<EnvironmentVariableTarget>("a"));
        }
    }
}
