using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.IO;

namespace Simple.Tests.IO
{
    public class CommandLineReaderFixture
    {
        [Test]
        public void CanReadSomeIntValues()
        {
            var reader = new CommandLineReader("/a:1", "-b:2", "-c=3");
            reader.Get("a", 0).Should().Be(1);
            reader.Get("b", 0).Should().Be(2);
            reader.Get("c", 0).Should().Be(3);
            reader.Get("d", 0).Should().Be(0);
        }

        [Test]
        public void CanReadSomeNullableIntValues()
        {
            var reader = new CommandLineReader("/a:1", "-b:2", "--c", "3");
            reader.Get<int?>("a").Should().Be(1);
            reader.Get<int?>("b").Should().Be(2);
            reader.Get<int?>("c").Should().Be(3);
            reader.Get<int?>("d").Should().Be(null);
        }

        [Test]
        public void CanReadSomeDateTimeValues()
        {
            var reader = new CommandLineReader("/a:11/11/2000");
            reader.Get("a", DateTime.Now).Should().Be(new DateTime(2000, 11, 11));
        }

        [Test]
        public void CanReadInvalidFormatValues()
        {
            var reader = new CommandLineReader("/a:asdasd");
            reader.Get("a", new DateTime(2000, 11, 11)).Should().Be(new DateTime(2000, 11, 11));
        }

        [Test]
        public void CanReadEnumValues()
        {
            var reader = new CommandLineReader("/a:machine");
            reader.Get<EnvironmentVariableTarget>("a").Should().Be(EnvironmentVariableTarget.Machine);
        }
    }
}
