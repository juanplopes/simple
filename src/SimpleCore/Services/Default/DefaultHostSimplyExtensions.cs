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
            SourceManager.Do.Register(config.ConfigKey, source);

            var hostProvider = new DefaultHostProvider();
            SourceManager.Do.AttachFactory(config.ConfigKey, hostProvider);

            var clientProvider = new DefaultClientProvider();
            SourceManager.Do.AttachFactory(config.ConfigKey, clientProvider);

            SourceManager.Do.Register(config.ConfigKey, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            SourceManager.Do.Register(config.ConfigKey, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

            return config;
        }

        public static SimplyRelease DefaultHost(this SimplyRelease config)
        {
            SourceManager.Do.Remove<DefaultHostConfig>(config.ConfigKey);
            SourceManager.Do.Remove<IServiceHostProvider>(config.ConfigKey);
            SourceManager.Do.Remove<IServiceClientProvider>(config.ConfigKey);
            return config;
        }


    }
}
