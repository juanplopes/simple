using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Services;
using System.Runtime.Remoting;
using log4net;
using Simple.Client;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace Simple.Remoting
{
    public class RemotingClientProvider : Factory<RemotingConfig>, IServiceClientProvider
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());

        protected override void OnConfig(RemotingConfig config)
        {
        }

        protected override void OnClearConfig()
        {
        }

        public object Create(Type type)
        {
            logger.DebugFormat("Creating remoting proxy to type {0}...", type.Name);

            Uri uriBase = ConfigCache.GetUriFromAddressBase();
            string relativeUri = ConfigCache.GetEndpointKey(type);
            Uri uriFinal;

            if (Uri.TryCreate(uriBase, relativeUri, out uriFinal))
            {
                logger.DebugFormat("Constructed URI: {0}...", uriFinal);

                return (MarshalByRefObject)RemotingServices.Connect(
                    type, uriFinal.AbsoluteUri);
            }
            throw new InvalidOperationException("Invalid uri pair in configuration: '" + 
                uriBase.AbsoluteUri + "' and '" + relativeUri + "'.");
        }
    }
}
