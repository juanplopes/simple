using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.Rules;
using System.ServiceModel;
using System.ServiceModel.Channels;
using SimpleLibrary.Config;
using System.Net;
using BasicLibrary.Logging;
using log4net;
using System.Reflection;
using BasicLibrary.DynamicProxy;

namespace SimpleLibrary.ServiceModel
{
    public class ServiceRulesProvider<T> : IRulesProvider<T>
        where T : ITestableService
    {
        protected ChannelFactory<T> FactoryCache { get; set; }
        protected SimpleLibraryConfig Config { get; set; }
        protected Binding DefaultBinding { get; set; }
        protected object lockObj = new object();
        protected ILog logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public ServiceRulesProvider()
        {
            Config = SimpleLibraryConfig.Get();
            DefaultBinding = ConfigLoader.CreateDefaultBinding();
        }

        protected T CreateNew(Uri endpointAddress)
        {
            ChannelFactory<T> factory = CreateChannelFactory();
            return factory.CreateChannel(new EndpointAddress(endpointAddress));
        }

        protected T CreateNew()
        {
            return CreateNew(new Uri(new Uri(Config.ServiceModel.DefaultBaseAddress), typeof(T).Name));
        }

        public T Create()
        {
            lock (lockObj)
            {
                var factory = DynamicProxyFactory.Instance;
                return (T)factory.CreateProxy(null, (o, m, p) =>
                {
                    T obj = CreateNew();

                    try
                    {
                        return m.Invoke(obj, p);
                    }
                    finally
                    {
                        try
                        {
                            (obj as ICommunicationObject).Close();
                        }
                        catch { logger.Warn("Unable to close CommunicationObject"); }
                    }
                });
            }
        }

        protected ChannelFactory<T> CreateChannelFactory()
        {
            if (FactoryCache == null)
            {
                FactoryCache = new ChannelFactory<T>(DefaultBinding);
                FactoryCache.Faulted += (s, a) => ((ICommunicationObject)s).Abort();
                ConfigLoader.ApplyConfigurators(FactoryCache.Endpoint, Config.ServiceModel.DefaultEndpoint, true);
            }

            return FactoryCache;
        }
    }
}
