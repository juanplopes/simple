using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace Simple.Services.Remoting.Channels
{
    public class HttpChannelHandler : IChannelHandler
    {
        #region IChannelHandler Members

        public IChannelReceiver CreateServerChannel(int port)
        {
            return CreateServerChannel(DefaultName, port);
        }

        public IChannelReceiver CreateServerChannel(string name, int port)
        {
            return new HttpServerChannel(name, port, new BinaryServerFormatterSinkProvider());
        }

        public IChannelSender CreateClientChannel()
        {
            return CreateClientChannel(DefaultName);
        }

        public IChannelSender CreateClientChannel(string name)
        {
            return new HttpClientChannel(name, new BinaryClientFormatterSinkProvider());
        }

        public string DefaultName
        {
            get { return "http"; }
        }

        public string Scheme
        {
            get { return "http"; }
        }

        #endregion
    }
}
