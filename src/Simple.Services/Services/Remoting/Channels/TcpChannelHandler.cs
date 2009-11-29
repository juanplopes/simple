using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;

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
            return new TcpServerChannel(name, uri.Port, new BinaryServerFormatterSinkProvider());
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
