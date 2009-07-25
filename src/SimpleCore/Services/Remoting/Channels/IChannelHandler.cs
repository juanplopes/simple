using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;

namespace Simple.Services.Remoting.Channels
{
    public interface IChannelHandler
    {
        IChannelReceiver CreateServerChannel(int port);
        IChannelReceiver CreateServerChannel(string name, int port);
        IChannelSender CreateClientChannel();
        IChannelSender CreateClientChannel(string name);
        string DefaultName { get; }
        string Scheme { get; }
    }
}

