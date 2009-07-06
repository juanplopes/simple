using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace Simple.Remoting
{
    public class RemotingHostProvider : Factory<RemotingConfig>, IServiceHostProvider
    {
        IChannelReceiver channel = null;

        protected override void OnConfig(RemotingConfig config)
        {
            TryStopChannel();

            channel = config.GetChannel();
            ChannelServices.RegisterChannel(channel, false);
        }

        protected override void OnClearConfig()
        {
            TryStopChannel();
        }

        protected void TryStopChannel()
        {
            if (channel != null)
                channel.StopListening(null);
        }

        public void Host(Type type, Type contract)
        {
            string key = ConfigCache.GetEndpointKey(contract);
            RemotingConfiguration.RegisterWellKnownServiceType(type, key, WellKnownObjectMode.Singleton);
        }

        public void Init()
        {
        }

        public void Start()
        {
        }
    }
}
