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

namespace Simple.Logging
{
    public class MainLogger
    {
        protected static MultiLoggerFactory _factory;
        protected static MultiLoggerFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    if (SimpleLibConfig.IsLoading) return null;
                    _factory = new MultiLoggerFactory();
                }
                return _factory;
            }
        }

        public static ILog Get(Type type)
        {
            return TryGetLogger(x => x.Get(type));
        }

        public static ILog Get(string loggerName)
        {
            return TryGetLogger(x => x.Get(loggerName));
        }

        public static ILog Get(object obj)
        {
            return TryGetLogger(x => x.Get(obj));
        }

        public static ILog Get<T>()
        {
            return TryGetLogger(x => x.Get<T>());
        }

        protected delegate ILog GetLoggerDelegate(MultiLoggerFactory factory);
        protected static ILog TryGetLogger(GetLoggerDelegate @delegate)
        {
            MultiLoggerFactory factory = Factory;
            if (factory != null)
            {
                return @delegate.Invoke(Factory);
            }
            else
            {
                return LogManager.GetLogger("dummy");
            }
        }

    }
}
