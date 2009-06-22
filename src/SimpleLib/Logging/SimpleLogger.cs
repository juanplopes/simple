using System;
using log4net;
using Simple.Common;
using System.Reflection;
using Simple.ConfigSource;

namespace Simple.Logging
{
    public class SimpleLogger
    {
        protected static object _lockObj = new object();

        protected static Log4netFactory _factory;
        protected static Log4netFactory Factory
        {
            get
            {
                lock (_lockObj)
                {
                    if (_factory == null)
                    {
                        _factory = new Log4netFactory();
                        SourcesManager.Configure(_factory);
                    }
                    return _factory;
                }
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
