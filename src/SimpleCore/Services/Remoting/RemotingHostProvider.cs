using System;
using System.Collections.Generic;
using Simple.ConfigSource;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using log4net;
using Simple.Client;
using System.Reflection;
using Simple.Patterns;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using Simple.Services.Default;

namespace Simple.Services.Remoting
{
    public class RemotingHostProvider : Factory<RemotingConfig>, IServiceHostProvider, IDisposable
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        ServiceLocationFactory factory = new ServiceLocationFactory();
        
        bool started = false;
        IChannelReceiver channel = null;

        protected override void OnConfig(RemotingConfig config)
        {
            if (started)
            {
                Stop();
                Start();
            }
        }

        protected override void OnClearConfig()
        {
            if (started)
            {
                Stop();
            }
        }

        public void Host(object server, Type contract)
        {
            lock (this)
            {
                if (!(server is MarshalByRefObject)) throw new ArgumentException("The server class must inherit from MarshalByRefObject");
                if (!started) Start();

                factory.Set(server, contract);

                logger.DebugFormat("Registering contract {0}...", contract.Name);
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (started)
                {
                    channel.StopListening(null);
                    RemotingServices.Disconnect(factory);
                    ChannelServices.UnregisterChannel(channel);
                    started = false;
                }
            }
        }


        public void Start()
        {
            lock (this)
            {
                logger.DebugFormat("Initializing Remoting Channel...");
                channel = ConfigCache.GetChannel();
                ChannelServices.RegisterChannel(channel, false);
                RemotingServices.Marshal(factory, RemotingConfig.DefaultRemotingUrl, typeof(IServiceLocationFactory));
                channel.StartListening(null);
                started = true;
            }


        }

    }
}
