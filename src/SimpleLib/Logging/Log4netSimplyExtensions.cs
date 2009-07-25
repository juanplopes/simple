using System;
using log4net;
using Simple.Common;
using System.Reflection;
using Simple.ConfigSource;
using Simple.Patterns;
using Simple.Logging;

namespace Simple
{
    public static class Log4netSimplyExtensions
    {
        private static FactoryManager<Log4netFactory, Log4netConfig> manager =
            new FactoryManager<Log4netFactory, Log4netConfig>(() => new Log4netFactory());

        private static Log4netFactory Factory
        {
            get
            {
                return manager.SafeGet();
            }
        }

        public static ILog Log(this Simply simply, Type type)
        {
            return TryGetLogger(x => x.Log(type));
        }

        public static ILog Log(this Simply simply, string loggerName)
        {
            return TryGetLogger(x => x.Log(loggerName));
        }

        public static ILog Log(this Simply simply, object obj)
        {
            return TryGetLogger(x => x.Log(obj));
        }

        public static ILog Log<T>(this Simply simply)
        {
            return TryGetLogger(x => x.Log<T>());
        }

        public static ILog Log(this Simply simply, MemberInfo member)
        {
            return TryGetLogger(x => x.Log(member));
        }

        private delegate ILog GetLoggerDelegate(Log4netFactory factory);
        private static ILog TryGetLogger(GetLoggerDelegate @delegate)
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
