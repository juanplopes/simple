using System;
using System.Collections.Generic;
using Simple.Config;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using log4net;
using System.Reflection;
using Simple.Patterns;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using Simple.DynamicProxy;

namespace Simple.Services.Remoting
{
    public class RemotingHostProvider : RemotingBaseProvider, IServiceHostProvider
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        IList<object> services = new List<object>();
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

        public override void Dispose()
        {
            this.Stop();
            base.Dispose();
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

                services.Add(server);

                string key = ConfigCache.GetEndpointKey(contract);
                logger.DebugFormat("Registering contract {0} at endpoint {1}...", contract.Name, key);
                
                RemotingServices.Marshal(server as MarshalByRefObject, key, contract);

                logger.InfoFormat("{0} hosted", key);
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
                    foreach (MarshalByRefObject obj in services)
                        RemotingServices.Disconnect(obj);

                    ChannelServices.UnregisterChannel(channel);
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
                ChannelServices.RegisterChannel(channel, false);
                
                channel.StartListening(null);

                started = true;
            }


        }
    }
}
