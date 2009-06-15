using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using Simple.ConfigSource;
using log4net;
using log4net.Config;
using log4net.Repository;
using System.Reflection;

namespace Simple.Logging
{
    public class Log4netFactory : Factory<LoggerConfig>, ILog4netFactory
    {
        protected override void InitializeObjects(LoggerConfig config)
        {
            LogManager.ResetConfiguration();
            XmlConfigurator.Configure(config.Log4net);
        }

        public ILog GetLogger(string name)
        {
            base.CheckInitialized();
            return LogManager.GetLogger(name);
        }

        public ILog GetLogger(Type type)
        {
            base.CheckInitialized();
            return LogManager.GetLogger(type);
        }

        public ILog GetLogger(object obj)
        {
            return GetLogger(obj.GetType());
        }

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public ILog GetLogger(MemberInfo member)
        {
            return GetLogger(member.DeclaringType);
        }
    }
}
