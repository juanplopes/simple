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

namespace Simple.Services.Remoting
{
    public class RemotingHostProvider : Factory<RemotingConfig>, IServiceHostProvider, IDisposable
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        IList<Pair<Type, Type>> services = new List<Pair<Type, Type>>();
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

        public void Host(Type type, Type contract)
        {
            lock (this)
            {
                if (!started) Start();

                services.Add(new Pair<Type, Type>(type, contract));

                string key = ConfigCache.GetEndpointKey(contract);
                logger.DebugFormat("Registering type {0} with contract {1} at endpoint {2}...", type.Name, contract.Name, key);
                RemotingConfiguration.RegisterWellKnownServiceType(
                    type, key, WellKnownObjectMode.Singleton);

                logger.InfoFormat("{0} hosted:", key);
                foreach (string url in channel.GetUrlsForUri(key))
                {
                    logger.DebugFormat("* {0}.", url);
                }
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (started)
                {
                    channel.StopListening(null);
                    //Context.UnregisterDynamicProperty(DefaultDynamicProperty.PropertyName, null, null);
                    //ChannelServices.UnregisterChannel(channel);
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
                // ChannelServices.RegisterChannel(channel, false);
                
                channel.StartListening(null);

                started = true;
            }

        }

    }
}
