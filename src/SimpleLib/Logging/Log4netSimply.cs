using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;

namespace Simple.Logging
{
    public class Log4netSimply : Singleton<Log4netSimply>, ISimply<Log4netConfig>
    {
        #region ISimply<Log4netConfig> Members

        public void Default()
        {
            IConfigSource<Log4netConfig> source =
                XmlConfig.LoadXml<Log4netConfig>(DefaultConfigResource.Log4netConfig);
            Configure(source);
        }

        public void Mute()
        {
            Configure(NullConfigSource<Log4netConfig>.Instance);
        }

        public void Configure(IConfigSource<Log4netConfig> source)
        {
            SourceManager.Do.Register<Log4netConfig>(null, source);
        }

        public void Configure(object key, IConfigSource<Log4netConfig> source)
        {
            SourceManager.Do.Register<Log4netConfig>(key, source);
        }

        #endregion
    }
}
