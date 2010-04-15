using System;
using Simple.Config;
using System.Runtime.Remoting;
using log4net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Channels;
using Simple.DynamicProxy;
using Simple.Reflection;
using Simple.Services.Default;

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
            logger.DebugFormat("Trying to find local reference to type {0}...", TypesHelper.GetFlatClassName(type));

            object obj = ServiceLocationFactory.Do[ConfigCache].TryGet(type);
            if (obj != null)
            {
                logger.DebugFormat("Found it, returning...");
                return obj;
            }
            logger.DebugFormat("Creating remoting proxy to type {0}...", TypesHelper.GetFlatClassName(type));

            Uri uriBase = ConfigCache.GetUriFromAddressBase();
            string relativeUri = ConfigCache.GetEndpointKey(type);
            Uri uriFinal;

            

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

        public override object ProxyObject(object obj, Type type, IInterceptor intercept)
        {
            return DynamicProxyFactory.Instance.CreateProxy(obj, intercept.Intercept);
        }
       
    }
}
