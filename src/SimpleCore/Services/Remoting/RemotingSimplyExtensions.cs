using Simple.Patterns;
using Simple.Config;
using Simple.Services.Remoting;
using Simple.Services;

namespace Simple
{
    public static class RemotingSimplyExtensions
    {
        public static SimplyConfigure RemotingDefault(this SimplyConfigure config)
        {
            return Remoting(config).FromXmlString(DefaultConfigContent.RemotingConfig);
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
            config.The(source);

            var hostProvider = new RemotingHostProvider();
            var clientProvider = new RemotingClientProvider();

            config.Factory(hostProvider);
            config.Factory(clientProvider);

            config.The<IServiceHostProvider>().FromInstance(hostProvider);
            config.The<IServiceClientProvider>().FromInstance(clientProvider);

            return config;
        }

        public static SimplyRelease Remoting(this SimplyRelease release)
        {
            release.The<RemotingConfig>();
            release.The<IServiceHostProvider>();
            release.The<IServiceClientProvider>();
            return release;
        }


    }
}
