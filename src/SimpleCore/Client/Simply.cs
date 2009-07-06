using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Simple.Logging;
using System.Reflection;
using Simple.Services;
using Simple.ConfigSource;

namespace Simple.Client
{
    public class Simply : ClientSimplyBase<Simply>
    {

    }

    public class ClientSimplyBase<F> : AggregateFactory<F>, ILog4netFactory, IServiceClientFactory, IServiceHostFactory
        where F : AggregateFactory<F>, new()
    {
        #region Logger
        public ILog Log(object obj)
        {
            return LoggerManager.Get(obj);
        }
        public ILog Log(string name)
        {
            return LoggerManager.Get(name);
        }
        public ILog Log(MemberInfo member)
        {
            return LoggerManager.Get(member);
        }
        public ILog Log(Type type)
        {
            return LoggerManager.Get(type);
        }
        public ILog Log<T>()
        {
            return LoggerManager.Get<T>();
        }
        #endregion

        #region Services
        public void Host(Type type)
        {
            ServiceManager.Host(ConfigKey, type);
        }

        public T Connect<T>()
        {
            return ServiceManager.Connect<T>(ConfigKey);
        }

        public object Connect(Type type)
        {
            return ServiceManager.Connect(ConfigKey, type);
        }
        #endregion

    }
}
