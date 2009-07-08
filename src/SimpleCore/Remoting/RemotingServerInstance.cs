using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Simple.Client;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace Simple.Remoting
{
    public class RemotingServerInstance : MarshalByRefObject
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        IChannelReceiver channel = null;

        public IChannelReceiver GetChannel()
        {
            lock (this)
            {
                if (Config == null) throw new InvalidOperationException("Server not configured");
                if (channel == null)
                {
                    logger.DebugFormat("Initializing Remoting Channel...");
                    channel = Config.GetChannel();
                    channel.StartListening(null);
                }
                return channel;
            }
        }

        public RemotingConfig Config { get; set; }
        public RemotingServerInstance()
        {
            logger.DebugFormat("Initializing instance from AppDomain {0}...", AppDomain.CurrentDomain.FriendlyName);
        }

        public void AddType(Type type, Type contract)
        {
            lock (this)
            {
                string key = Config.GetEndpointKey(contract);
                logger.DebugFormat("Registering type {0} with contract {1} at endpoint {2}...", type.Name, contract.Name, key);
                RemotingConfiguration.RegisterWellKnownServiceType(type, key, WellKnownObjectMode.Singleton);

                logger.InfoFormat("{0} hosted:", key);
                foreach (string url in GetChannel().GetUrlsForUri(key))
                {
                    logger.DebugFormat("* {0}.", url);
                }

            }
        }

        public void TryStop()
        {
            try
            {
                logger.DebugFormat("Instructing channel to stop.");
                GetChannel().StopListening(null);
            }
            catch
            {
                logger.WarnFormat("Couldn't stop channel.");
            }
        }
    }
}
