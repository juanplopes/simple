using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

namespace Simple.Services.Remoting.Channels
{
    public class TcpChannelHandler : IChannelHandler
    {
        #region IChannelHandler Members

        public IChannelReceiver CreateServerChannel(Uri uri)
        {
            return CreateServerChannel(DefaultName, uri);
        }

        public IChannelReceiver CreateServerChannel(string name, Uri uri)
        {
            var sink = new BinaryServerFormatterSinkProvider();
            sink.TypeFilterLevel = TypeFilterLevel.Full;

            return new TcpServerChannel(name, uri.Port, sink);
        }

        public IChannelSender CreateClientChannel()
        {
            return CreateClientChannel(DefaultName);
        }

        public IChannelSender CreateClientChannel(string name)
        {
            return new TcpClientChannel(name, new BinaryClientFormatterSinkProvider());
        }

        public string DefaultName
        {
            get { return "tcp"; }
        }

        public string Scheme
        {
            get { return "tcp"; }
        }

        #endregion
    }
}
