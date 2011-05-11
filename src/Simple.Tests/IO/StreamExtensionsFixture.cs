using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using System.IO;
using Simple.IO;

namespace Simple.Tests.IO
{
    [TestFixture]
    public class StreamExtensionsFixture
    {
        [Test]
        public void CanReadZeroByteStream()
        {
            var stream = new MemoryStream();
            var bytes = stream.ToByteArray(1000);
            bytes.Length.Should().Be(0);
        }

        [Test]
        public void CanReadTwoByteStream()
        {
            var stream = new MemoryStream(new byte[] { 0x30, 143 });
            var bytes = stream.ToByteArray(1000);
            bytes.Should().Have.SameSequenceAs<byte>(0x30, 143);
        }

        [Test]
        public void CanReadTwoByteStreamWithoutInitialLimit()
        {
            var stream = new MemoryStream(new byte[] { 0x30, 143 });
            var bytes = stream.ToByteArray(-1);
            bytes.Should().Have.SameSequenceAs<byte>(0x30, 143);
        }

        [Test]
        public void CanReadMoreBytesThanTheBuffer()
        {
            var stream = new MemoryStream(Enumerable.Range(0, 256).Select(x => Convert.ToByte(x)).ToArray());
            var bytes = stream.ToByteArray(2);
            bytes.Should().Have.UniqueValues().And.Have.Count.EqualTo(256);
        }

        [Test]
        public void CanTransformStringToStream()
        {
            var stream = "abcdef".ToStream();
            stream.ToByteArray(10).Should().Have.SameSequenceAs(Encoding.Default.GetBytes("abcdef"));
        }

        [Test]
        public void CanTransformStringToStreamWithCustomEncoding()
        {
            var stream = "abcdef".ToStream(Encoding.UTF32);
            stream.ToByteArray(10).Should().Have.SameSequenceAs(Encoding.UTF32.GetBytes("abcdef"));
            stream.ToByteArray(10).Should().Not.Have.SameSequenceAs(Encoding.ASCII.GetBytes("abcdef"));
        }

        [Test]
        public void CanWriteIntoStringStreamAndConvertItBackToString()
        {
            Encoding.UTF32.GetString(Encoding.UTF32.GetBytes("wtf")).Should().Be("wtf");

            var stream = new StringStream();
            var writer = new StreamWriter(stream);
            writer.Write("qweqwe");
            writer.Flush();
            stream.GetString().Should().Be("qweqwe");
            stream.GetString().Should().Be("qweqwe");
        }

        [Test, Ignore]
        public void CanWriteIntoStringStreamAndConvertItBackToStringWithCustomEncoding()
        {
            var stream = new StringStream(Encoding.UTF32);
            var writer = new StreamWriter(stream, Encoding.UTF32);
            writer.Write("qweqwe");
            writer.Flush();
            stream.GetString().Should().Be("qweqwe");
            stream.GetString().Should().Be("qweqwe");
        }

    }
}
