using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Services;
using System.Runtime.Remoting;

namespace Simple.Remoting
{
    public class RemotingClientProvider : Factory<RemotingConfig>, IServiceClientProvider
    {
        protected override void OnConfig(RemotingConfig config)
        {
        }

        protected override void OnClearConfig()
        {
        }

        public object Create(Type type)
        {
            Uri uriBase = ConfigCache.GetUriFromAddressBase();
            string relativeUri = ConfigCache.GetEndpointKey(type);
            Uri uriFinal;

            if (Uri.TryCreate(uriBase, relativeUri, out uriFinal))
            {
                return (MarshalByRefObject)RemotingServices.Connect(
                    type, uriFinal.AbsoluteUri);
            }
            throw new InvalidOperationException("Invalid uri pair in configuration: '" + 
                uriBase.AbsoluteUri + "' and '" + relativeUri + "'.");
        }
    }
}
