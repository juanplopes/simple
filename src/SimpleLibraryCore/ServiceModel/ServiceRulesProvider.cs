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

namespace SimpleLibrary.ServiceModel
{
    public class ServiceRulesProvider<T> : IRulesProvider<T>
        where T:ITestableService
    {
        protected ChannelFactory<T> FactoryCache { get; set; }
        protected SimpleLibraryConfig Config { get; set; }
        protected Binding DefaultBinding { get; set; }
        protected T Cache { get; set; }
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

        public T Create()
        {
            lock (lockObj)
            {
                if (Cache == null || (Cache as ICommunicationObject).State != CommunicationState.Opened ||
                    FactoryCache == null || FactoryCache.State != CommunicationState.Opened)
                {
                    logger.Debug("The channel was in an invalid state. Refreshing...");
                    Cache = CreateNew(new Uri(new Uri(Config.ServiceModel.DefaultBaseAddress), typeof(T).Name));
                }
                else
                {
                    try
                    {
                        Cache.HeartBeat();
                    }
                    catch (CommunicationException)
                    {
                        logger.Debug("Heartbeat failed. Refreshing...");
                        Cache = CreateNew(new Uri(new Uri(Config.ServiceModel.DefaultBaseAddress), typeof(T).Name));
                    }
                    catch (TimeoutException)
                    {
                        logger.Debug("Heartbeat timed out. Refreshing...");
                        Cache = CreateNew(new Uri(new Uri(Config.ServiceModel.DefaultBaseAddress), typeof(T).Name));
                    }
                }
                return Cache;
            }
        }

        protected ChannelFactory<T> CreateChannelFactory()
        {
            if (FactoryCache == null)
            {
                FactoryCache = new ChannelFactory<T>(DefaultBinding);
                ConfigLoader.ApplyConfigurators(FactoryCache.Endpoint, Config.ServiceModel.DefaultEndpoint, true);
            }

            return FactoryCache;
        }
    }
}
