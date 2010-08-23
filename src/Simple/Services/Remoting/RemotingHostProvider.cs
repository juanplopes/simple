using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using log4net;
using Simple.Services.Default;

namespace Simple.Services.Remoting
{
    public class RemotingHostProvider : RemotingBaseProvider, IServiceHostProvider
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        IList<object> services = new List<object>();
        bool started = false;
        bool wasStared = false;

        IChannelReceiver channel = null;

        protected override void OnDisposeOldConfig()
        {
            lock (this)
                if (started)
                {
                    Stop();
                    wasStared = true;
                }
        }

        protected override void OnConfig(RemotingConfig config)
        {
            lock (this)
                if (wasStared)
                {
                    wasStared = false;
                    Start();
                }
        }

        public override void Dispose()
        {
            this.Stop();
            base.Dispose();
        }

        public void Host(object server, Type contract)
        {
            lock (this)
            {
                if (!(server is MarshalByRefObject)) throw new ArgumentException("The server class must inherit from MarshalByRefObject");
                if (!started) Start();

                services.Add(server);
                ServiceLocationFactory.Do[ConfigCache].Set(server, contract);

                string key = ConfigCache.GetEndpointKey(contract);

                if (!ConfigCache.LocalOnly)
                {
                    logger.DebugFormat("Registering contract {0} at endpoint {1}...", contract.GetRealClassName(), key);
                    RemotingServices.Marshal(server as MarshalByRefObject, key, contract);
                    logger.InfoFormat("{0} hosted", key);

                    foreach (string url in channel.GetUrlsForUri(key))
                    {
                        logger.DebugFormat("* {0}.", url);
                    }
                }
                else
                {
                    logger.InfoFormat("{0} locally hosted", key);
                }

            }
        }



        public void Stop()
        {
            lock (this)
            {
                if (!started) return;

                if (!ConfigCache.LocalOnly)
                {
                    foreach (MarshalByRefObject obj in services)
                        RemotingServices.Disconnect(obj);

                    ChannelServices.UnregisterChannel(channel);
                    channel.StopListening(null);
                }
                //Context.UnregisterDynamicProperty(DefaultDynamicProperty.PropertyName, null, null);
                //ChannelServices.UnregisterChannel(channel);
                started = false;
            }

        }


        public void Start()
        {
            lock (this)
            {
                if (started) return;

                if (!ConfigCache.LocalOnly)
                {
                    logger.DebugFormat("Initializing Remoting Channel...");
                    channel = ConfigCache.GetChannel();
                    ChannelServices.RegisterChannel(channel, false);
                    channel.StartListening(null);
                }

                started = true;
            }


        }
    }
}
