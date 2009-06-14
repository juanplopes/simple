using System;
using System.Collections.Generic;
using System.Text;
using Simple.Config;
using Simple.Configuration2;
using log4net;
using log4net.Config;

namespace Simple.Logging
{
    public class MultiLoggerFactory
    {
        protected SimpleLibConfig Config { get; set; }

        public MultiLoggerFactory()
        {
            if (SimpleLibConfig.IsLoading) throw new InvalidOperationException("Cannot use logger until basic library is done.");
            LoadConfig();
        }

        protected void LoadConfig()
        {
            lock (this)
            {
                Config = SimpleLibConfig.Get();
                (Config as IConfigElement).OnExpire += new EventHandler(MultiLoggerFactory_OnExpire);
                LogManager.ResetConfiguration();
                XmlConfigurator.Configure(Config.Log4net.GetStream());
            }
        }

        protected void MultiLoggerFactory_OnExpire(object sender, EventArgs e)
        {
            LoadConfig();
        }

        public ILog Get(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public ILog Get(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }

        public ILog Get(object obj)
        {
            return Get(obj.GetType());
        }

        public ILog Get<T>()
        {
            return Get(typeof(T));
        }
    }
}
