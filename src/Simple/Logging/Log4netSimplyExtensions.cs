using System;
using log4net;
using Simple.Common;
using System.Reflection;
using Simple.Config;
using Simple.Patterns;
using Simple.Logging;

namespace Simple
{
    public static class Log4netSimplyExtensions
    {
        #region Simply
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
        #endregion
        #region Configure
        public static SimplyConfigure Log4netToConsole(this SimplyConfigure config)
        {
            IConfigSource<Log4netConfig> source =
                XmlConfig.LoadXml<Log4netConfig>(Resources.Log4netConfig);
            return Log4net(config, source);
        }

        public static IConfiguratorInterface<Log4netConfig, SimplyConfigure> Log4net(this SimplyConfigure config)
        {
            return new ConfiguratorInterface<Log4netConfig, SimplyConfigure>(x => Log4net(config, x));
        }

        public static SimplyConfigure Log4net(this SimplyConfigure config, string file)
        {
            return Log4net(config, XmlConfig.LoadFile<Log4netConfig>(file));
        }

        public static SimplyConfigure Log4net(this SimplyConfigure config, IConfigSource<Log4netConfig> source)
        {
            SourceManager.Do.Register<Log4netConfig>(null, source); //log4net better only one
            return config;
        }

        #endregion

        public static SimplyRelease Log4net(this SimplyRelease config)
        {
            SourceManager.Do.Register<Log4netConfig>(config.ConfigKey, NullConfigSource<Log4netConfig>.Instance);
            return config;
        }

    }
}
