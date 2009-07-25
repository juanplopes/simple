using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;

namespace Simple.Services.Remoting.Channels
{
    public class IpcChannelHandler : IChannelHandler
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
            return new IpcServerChannel(name, uri.Host,sink);
        }

        public IChannelSender CreateClientChannel()
        {
            return CreateClientChannel(DefaultName);
        }

        public IChannelSender CreateClientChannel(string name)
        {
            return new IpcClientChannel(name, new BinaryClientFormatterSinkProvider());
        }

        public string DefaultName
        {
            get { return "ipc"; }
        }

        public string Scheme
        {
            get { return "ipc"; }
        }

        #endregion
    }
}
