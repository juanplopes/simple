using System;
using Simple.ConfigSource;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Simple.Logging
{
    public class Log4netFactory : Factory<Log4netConfig>
    {
        protected override void OnConfig(Log4netConfig config)
        {
            LogManager.ResetConfiguration();
            XmlConfigurator.Configure(config.Element);
        }

        protected override void OnClearConfig()
        {
            IConfigSource<Log4netConfig> source = new XmlConfigSource<Log4netConfig>().Load(
                DefaultConfigResource.Log4netConfig);

            OnConfig(source.Get());
        }

        public ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }

        public ILog GetLogger(Type type)
        {
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
