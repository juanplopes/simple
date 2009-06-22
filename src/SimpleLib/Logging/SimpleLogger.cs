using System;
using log4net;
using Simple.Common;
using System.Reflection;
using Simple.ConfigSource;
using Simple.Patterns;

namespace Simple.Logging
{
    public class SimpleLogger
    {
        private static FactoriesManager<Log4netFactory, Log4netConfig> manager =
            new FactoriesManager<Log4netFactory, Log4netConfig>(() => new Log4netFactory());

        protected static Log4netFactory Factory
        {
            get
            {
                return manager.SafeGet();
            }
        }

        public static ILog Get(Type type)
        {
            return TryGetLogger(x => x.GetLogger(type));
        }

        public static ILog Get(string loggerName)
        {
            return TryGetLogger(x => x.GetLogger(loggerName));
        }

        public static ILog Get(object obj)
        {
            return TryGetLogger(x => x.GetLogger(obj));
        }

        public static ILog Get<T>()
        {
            return TryGetLogger(x => x.GetLogger<T>());
        }

        public static ILog Get(MemberInfo member)
        {
            return TryGetLogger(x => x.GetLogger(member));
        }

        protected delegate ILog GetLoggerDelegate(Log4netFactory factory);
        protected static ILog TryGetLogger(GetLoggerDelegate @delegate)
        {
            Log4netFactory factory = Factory;
            if (factory != null)
            {
                return @delegate.Invoke(Factory);
            }
            else
            {
                return Singleton<NullLog>.Instance;
            }
        }

    }
}
