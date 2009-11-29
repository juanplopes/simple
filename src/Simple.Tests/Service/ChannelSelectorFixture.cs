using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Services.Remoting;
using Simple.Services.Remoting.Channels;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class ChannelSelectorFixture
    {
        [Test]
        public void TestTcp()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("tcp://localhost"));
            Assert.IsInstanceOf(typeof(TcpChannelHandler), handler);
        }

        [Test]
        public void TestHttp()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("http://localhost"));
            Assert.IsInstanceOf(typeof(HttpChannelHandler), handler);
        }

        [Test]
        public void TestIpc()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("ipc://localhost"));
            Assert.IsInstanceOf(typeof(IpcChannelHandler), handler);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestNotKnown()
        {
            var handler = ChannelSelector.Do.GetHandler(new Uri("whatever://localhost"));
        }

    }
}
