using Simple.Patterns;
using Simple.ConfigSource;
using Simple.Services.Remoting;
using Simple.Services;
using Simple.Services.Default;

namespace Simple
{
    public static class DefaultHostSimplyExtensions
    {
        public static SimplyConfigure DefaultHost(this SimplyConfigure config)
        {
            return DefaultHost(config, 
                new DirectConfigSource<DefaultHostConfig>().Load(new DefaultHostConfig()));
        }

        public static SimplyConfigure DefaultHost(this SimplyConfigure config, IConfigSource<DefaultHostConfig> source)
        {
            config.The(source);
            //SourceManager.Do.Register(config.ConfigKey, source);

            var hostProvider = new DefaultHostProvider();
            var clientProvider = new DefaultClientProvider();

            config.Factory(hostProvider);
            config.Factory(clientProvider);

            //SourceManager.Do.AttachFactory(config.ConfigKey, hostProvider);
            //SourceManager.Do.AttachFactory(config.ConfigKey, clientProvider);

            config.The<IServiceHostProvider>().FromInstance(hostProvider);
            config.The<IServiceClientProvider>().FromInstance(clientProvider);

            //SourceManager.Do.Register(config.ConfigKey, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            //SourceManager.Do.Register(config.ConfigKey, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

            return config;
        }

        public static SimplyRelease DefaultHost(this SimplyRelease release)
        {
            release.The<DefaultHostConfig>();
            release.The<IServiceHostProvider>();
            release.The<IServiceClientProvider>();
            return release;
        }


    }
}
