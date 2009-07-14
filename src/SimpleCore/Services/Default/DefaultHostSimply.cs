using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;

namespace Simple.Services.Default
{
    public class DefaultHostSimply : Singleton<DefaultHostSimply>, ISimply<DefaultHostConfig>
    {
        #region ISimply<RemotingConfig> Members

        public void Configure(object key, IConfigSource<DefaultHostConfig> source)
        {
            SourceManager.Do.Register(key, source);

            var hostProvider = new DefaultHostProvider();
            SourceManager.Do.AttachFactory(key, hostProvider);

            var clientProvider = new DefaultClientProvider();
            SourceManager.Do.AttachFactory(key, clientProvider);

            SourceManager.Do.Register(key, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            SourceManager.Do.Register(key, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

        }

        #endregion
    }
}
