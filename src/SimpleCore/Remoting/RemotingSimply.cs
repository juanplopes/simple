using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;
using Simple.Services;

namespace Simple.Remoting
{
    public class RemotingSimply : Singleton<RemotingSimply>, ISimply<RemotingConfig>
    {
        #region ISimply<RemotingConfig> Members

        public void Configure(object key, IConfigSource<RemotingConfig> source)
        {
            SourceManager.RegisterSource(key, source);

            var hostProvider = new RemotingHostProvider();
            SourceManager.Configure(key, hostProvider);

            var clientProvider = new RemotingClientProvider();
            SourceManager.Configure(key, clientProvider);

            SourceManager.RegisterSource(key, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            SourceManager.RegisterSource(key, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

        }

        #endregion
    }
}
