using System;
using Simple.ConfigSource;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Simple.Logging
{
    public class Log4netFactory : Factory<Log4netConfig>, Simple.Logging.ILog4netFactory
    {
        protected override void OnConfig(Log4netConfig config)
        {
            LogManager.ResetConfiguration();
            if (config != null)
                XmlConfigurator.Configure(config.Element);
        }

        protected override void OnClearConfig()
        {
            IConfigSource<Log4netConfig> source = new XmlConfigSource<Log4netConfig>().Load(
                DefaultConfigResource.Log4netConfig);

            OnConfig(source.Get());
        }

        public ILog Log(string name)
        {
            return LogManager.GetLogger(name);
        }

        public ILog Log(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public ILog Log(object obj)
        {
            return Log(obj.GetType());
        }

        public ILog Log<T>()
        {
            return Log(typeof(T));
        }

        public ILog Log(MemberInfo member)
        {
            return Log(member.DeclaringType);
        }




    }
}
