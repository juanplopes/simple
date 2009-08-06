using System;
using Simple.ConfigSource;
using System.Runtime.Remoting;
using log4net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Channels;
using Simple.DynamicProxy;

namespace Simple.Services.Remoting
{
    public class RemotingClientProvider : RemotingBaseProvider, IServiceClientProvider
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

            object obj = null;

            if (Uri.TryCreate(uriBase, relativeUri, out uriFinal))
            {
                logger.DebugFormat("Constructed URI: {0}...", uriFinal);

                ConfigCache.TryRegisterClientChannel();
                obj = RemotingServices.Connect(type, uriFinal.AbsoluteUri);

                return obj;
            }
            throw new InvalidOperationException("Invalid uri pair in configuration: '" +
                uriBase.AbsoluteUri + "' and '" + relativeUri + "'.");
        }

        public override object ProxyObject(object obj, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateProxy(obj, intercept.Intercept);
        }
       
    }
}
