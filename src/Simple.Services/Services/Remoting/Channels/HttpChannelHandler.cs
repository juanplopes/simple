using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using Simple.Services.Remoting.Serialization;

namespace Simple.Services.Remoting.Channels
{
    public class HttpChannelHandler : IChannelHandler
    {
        #region IChannelHandler Members

        public IChannelReceiver CreateServerChannel(Uri uri)
        {
            return CreateServerChannel(DefaultName, uri);
        }

        public IChannelReceiver CreateServerChannel(string name, Uri uri)
        {
            return new HttpServerChannel(name, uri.Port, new CustomBinaryServerFormatterSinkProvider());
        }

        public IChannelSender CreateClientChannel()
        {
            return CreateClientChannel(DefaultName);
        }

        public IChannelSender CreateClientChannel(string name)
        {
            return new HttpClientChannel(name, new CustomBinaryClientFormatterSinkProvider());
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
