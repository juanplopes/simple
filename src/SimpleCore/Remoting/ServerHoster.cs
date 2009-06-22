using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;

namespace Simple.Remoting
{
    public class ServerHoster
    {
        public IChannelReceiver Channel { get; set; }
        public ServerHoster(object config)
        {
            
        }

        public void Initialize()
        {
            Channel = new TcpChannel(8080);
            ChannelServices.RegisterChannel(Channel, false);
            Channel.StartListening(null);
        }

        public void AddService(Type type, string endpointKey)
        {
            RemotingConfiguration.RegisterWellKnownServiceType(type, endpointKey, WellKnownObjectMode.Singleton);
        }
    }
}
