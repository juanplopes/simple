using Simple.Patterns;
using Simple.ConfigSource;

namespace Simple.Services.Remoting
{
    public class RemotingSimply : Singleton<RemotingSimply>, ISimply<RemotingConfig>
    {
        #region ISimply<RemotingConfig> Members

        public void Configure(object key, IConfigSource<RemotingConfig> source)
        {
            SourceManager.Do.Register(key, source);

            var hostProvider = new RemotingHostProvider();
            SourceManager.Do.AttachFactory(key, hostProvider);

            var clientProvider = new RemotingClientProvider();
            SourceManager.Do.AttachFactory(key, clientProvider);

            SourceManager.Do.Register(key, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            SourceManager.Do.Register(key, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

        }

        #endregion

        public void ReleaseConfig(object key)
        {
            SourceManager.Do.Remove<RemotingConfig>(key);
            SourceManager.Do.Remove<IServiceHostProvider>(key);
            SourceManager.Do.Remove<IServiceClientProvider>(key);
        }
    }
}
