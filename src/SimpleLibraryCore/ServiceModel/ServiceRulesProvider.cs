using System;
using System.Collections.Generic;

using System.Text;
using Simple.Rules;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Simple.Config;
using System.Net;
using Simple.Logging;
using log4net;
using System.Reflection;
using Simple.DynamicProxy;
using System.Diagnostics;
using Simple.Common;

namespace Simple.ServiceModel
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
        
        [DebuggerHidden]
        public T Create()
        {
            lock (lockObj)
            {
                var factory = DynamicProxyFactory.Instance;
                return (T)factory.CreateProxy(null, (nullObject, method, parameters) =>
                {
                    T obj = CreateNew();

                    try
                    {
                        SimpleContext.Get().Refresh(true);
                        return method.Invoke(obj, parameters);
                    }
                    catch (TargetInvocationException e)
                    {
                        throw ExHelper.ForReal(e);
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
