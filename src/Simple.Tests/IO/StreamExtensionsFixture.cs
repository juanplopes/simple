using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using System.IO;

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
    }
}
