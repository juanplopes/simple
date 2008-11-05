using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using log4net;
using log4net.Config;
using System.Configuration;
using System.IO;
using BasicLibrary.LibraryConfig;
using BasicLibrary.Common;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;
using BasicLibrary.Configuration;
using System.Reflection;

namespace BasicLibrary.Logging
{
    public class MainLogger
    {
        static MainLogger()
        {
            BasicLibraryConfig config = BasicLibraryConfig.Get();
            XmlConfigurator.Configure(config.Log4net.GetStream());
        }

        public static ILog Get(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static ILog Get(string loggerName) {
            return LogManager.GetLogger(loggerName);
        }

        public static ILog Get(object obj)
        {
            return Get(obj.GetType());
        }

        public static ILog Get<T>()
        {
            return Get(typeof(T));
        }

    }
}
