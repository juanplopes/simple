using System;
using Simple.ConfigSource;
using System.Runtime.Remoting;
using log4net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;

namespace Simple.Services.Remoting
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

                try
                {
                    var obj = RemotingServices.Connect(type, uriFinal.AbsoluteUri);
                    obj.Equals(null);
                   
                    return obj;
                }
                catch (RemotingException e)
                {
                    throw new ServiceConnectionException(e.Message, e);
                }
                catch (NullReferenceException e)
                {
                    throw new ServiceConnectionException(e.Message, e);
                }
            }
            throw new InvalidOperationException("Invalid uri pair in configuration: '" + 
                uriBase.AbsoluteUri + "' and '" + relativeUri + "'.");
        }
    }
}
