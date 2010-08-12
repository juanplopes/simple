using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Services.Remoting;
using Simple.Services.Remoting.Channels;

namespace Simple.Tests.Services
{
    [TestFixture]
    public class ChannelSelectorFixture
    {
        [Test]
        public void TestTcp()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("tcp://localhost"));
            handler.Should().Be.InstanceOf<TcpChannelHandler>();
        }

        [Test]
        public void TestHttp()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("http://localhost"));
            handler.Should().Be.InstanceOf<HttpChannelHandler>();
        }

        [Test]
        public void TestIpc()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("ipc://localhost"));
            handler.Should().Be.InstanceOf<IpcChannelHandler>();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestNotKnown()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("whatever://localhost"));
        }

    }
}
