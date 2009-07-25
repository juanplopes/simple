using System;
using Simple.ConfigSource;
using System.Runtime.Remoting;
using log4net;
using Simple.Client;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using Simple.Services.Default;

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

            Uri uri = ConfigCache.GetUriFromAddressBase();
            Uri.TryCreate(uri, RemotingConfig.DefaultRemotingUrl, out uri);

            logger.DebugFormat("Resolving at URI: {0}...", uri);

            try
            {
                var factory = (IServiceLocationFactory)RemotingServices
                    .Connect(typeof(IServiceLocationFactory), uri.AbsoluteUri);

                return factory.Get(type);
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
    }
}
