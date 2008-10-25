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
    [IgnoreLogger]
    public class MainLogger
    {
        static MainLogger()
        {
            BasicLibraryConfig config = BasicLibraryConfig.Get();
            XmlConfigurator.Configure(config.Log4net.GetStream());
        }

        public static ILog Default
        {
            get
            {
                string loggerName = GetLoggerFromCallingMethod(new StackTrace());
                return GetLogger(loggerName);
            }
        }

        public static ILog GetLogger(string loggerName) {
            return LogManager.GetLogger(loggerName);
        }

        protected static string GetLoggerFromCallingMethod(StackTrace trace)
        {
            for (int i = 0; i < trace.FrameCount; i++)
            {
                MethodBase method = trace.GetFrame(i).GetMethod();
                if (!method.IsDefined(typeof(IgnoreLoggerAttribute), true)
                    && !(method.DeclaringType.IsDefined(typeof(IgnoreLoggerAttribute), true)))
                    return method.DeclaringType.FullName;
            }
            throw new ApplicationException("Couldn't get method from stack trace.");
        }
    }
}
