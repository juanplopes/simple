using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Simple.ServiceModel;
using Simple.Config;
using Simple.Logging;
using Simple.Configuration;
using log4net;
using System.Reflection;

namespace Simple.Rules
{
    public static class RulesFactory
    {
        private static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        private static Dictionary<Type, object> ProviderCache = new Dictionary<Type, object>();
        private static SimpleConfig Config = SimpleConfig.Get();

        public static T Create<T>() where T : class, ITestableService
        {
            IRulesProvider<T> provider = null;

            if (ProviderCache.ContainsKey(typeof(T)))
            {
                provider = (IRulesProvider<T>)ProviderCache[typeof(T)];
            }
            else
            {
                foreach (TypeConfigElement typeElement in Config.Business.RulesFactories)
                {
                    try
                    {
                        Type type = typeElement.LoadType();
                        if (type != null)
                        {
                            provider = (IRulesProvider<T>)Activator.CreateInstance(type.MakeGenericType(typeof(T)));
                            break;
                        }
                        else
                        {
                            Logger.WarnFormat("Couldn't load provider type {0}", typeElement.Name);
                        }
                    }
                    catch (Exception)
                    {
                        Logger.WarnFormat("Couldn't load provider type {0}", typeElement.Name);
                    }
                }

                if (provider == null)
                {
                    provider = new ServiceRulesProvider<T>();
                }

                ProviderCache[typeof(T)] = provider;
            }

            return provider.Create();
        }
    }
}
