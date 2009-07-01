using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Simple.ServiceModel;
using Simple.Config;
using Simple.Logging;
using Simple.Configuration2;
using log4net;
using System.Reflection;
using Simple.DynamicProxy;

namespace Simple.Services
{
    public static class RulesFactory
    {
        private static ILog Logger = LoggerManager.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        private static Dictionary<Type, object> ProviderCache = new Dictionary<Type, object>();
        private static SimpleConfig Config = SimpleConfig.Get();

        public static T Create<T>() where T : class
        {
            return (T)DynamicProxyFactory.Instance.CreateProxy(null, (o, m, p) =>
            {
                throw new NotImplementedException();
            });
        }
    }
}
