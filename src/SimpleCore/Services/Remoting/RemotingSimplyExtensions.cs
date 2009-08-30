using Simple.Patterns;
using Simple.ConfigSource;
using Simple.Services.Remoting;
using Simple.Services;

namespace Simple
{
    public static class RemotingSimplyExtensions
    {
        public static SimplyConfigure RemotingDefault(this SimplyConfigure config)
        {
            return Remoting(config).FromXml(DefaultConfigContent.RemotingConfig);
        }

        public static IConfiguratorInterface<RemotingConfig, SimplyConfigure> Remoting(this SimplyConfigure config)
        {
            return new ConfiguratorInterface<RemotingConfig, SimplyConfigure>(x => Remoting(config, x));
        }

        public static SimplyConfigure Remoting(this SimplyConfigure config, string baseUrl)
        {
            return Remoting(config).FromInstance(new RemotingConfig() { BaseAddress = baseUrl });
        }

        public static SimplyConfigure Remoting(this SimplyConfigure config, IConfigSource<RemotingConfig> source)
        {
            SourceManager.Do.Register(config.ConfigKey, source);

            var hostProvider = new RemotingHostProvider();
            var clientProvider = new RemotingClientProvider();

            SourceManager.Do.AttachFactory(config.ConfigKey, hostProvider);
            SourceManager.Do.AttachFactory(config.ConfigKey, clientProvider);

            SourceManager.Do.Register(config.ConfigKey, new DirectConfigSource<IServiceHostProvider>().Load(hostProvider));
            SourceManager.Do.Register(config.ConfigKey, new DirectConfigSource<IServiceClientProvider>().Load(clientProvider));

            return config;
        }

        public static SimplyRelease Remoting(this SimplyRelease config)
        {
            SourceManager.Do.Remove<RemotingConfig>(config.ConfigKey);
            SourceManager.Do.Remove<IServiceHostProvider>(config.ConfigKey);
            SourceManager.Do.Remove<IServiceClientProvider>(config.ConfigKey);
            return config;
        }


    }
}
