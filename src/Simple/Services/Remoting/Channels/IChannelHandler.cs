using System;
using System.Runtime.Remoting.Channels;

namespace Simple.Services.Remoting.Channels
{
    public interface IChannelHandler
    {
        IChannelReceiver CreateServerChannel(Uri uri);
        IChannelReceiver CreateServerChannel(string name, Uri uri);
        IChannelSender CreateClientChannel();
        IChannelSender CreateClientChannel(string name);
        string DefaultName { get; }
        string Scheme { get; }
    }
}

