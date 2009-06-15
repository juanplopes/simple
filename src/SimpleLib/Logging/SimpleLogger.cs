using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using log4net;
using log4net.Config;
using System.Configuration;
using System.IO;
using Simple.Config;
using Simple.Common;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;
using Simple.Configuration2;
using System.Reflection;
using Simple.ConfigSource;

namespace Simple.Logging
{
    public class SimpleLogger
    {
        protected static Log4netFactory _factory;
        protected static Log4netFactory Factory
        {
            get
            {
                if (!SourcesManager<LoggerConfig>.HasSource())
                    return null;

                if (_factory == null)
                {
                    _factory = SourcesManager<LoggerConfig>.GetFactory<Log4netFactory>();
                }
                return _factory;
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
