using System;
using System.IO;
using System.Reflection;
using Simple.Config;
using Simple.IO;

namespace Simple
{
    public abstract class ConfigDef
    {
        private FileLocator locations = null;
        protected FileLocator Locations
        {
            get
            {
                if (locations == null)
                {
                    locations = new FileLocator();
                    InitLocations(locations);
                }
                return locations;
            }
        }
        public string Environment { get; protected set; }

        public const string Test = "Test";
        public bool IsTest { get { return Is(Test); } }

        public const string Development = "Development";
        public bool IsDevelopment { get { return Is(Development); } }

        public const string Production = "Production";
        public bool IsProduction { get { return Is(Production); } }

        public virtual string DefaultEnvironment { get { return Development; } }
        public bool IsDefault { get { return Is(DefaultEnvironment); } }

        public bool Is(string env)
        {
            env = env ?? DefaultEnvironment;
            return string.Compare(Environment, env, true) == 0;
        }


        public ConfigDef(string env)
        {
            Environment = env ?? DefaultEnvironment;
        }

        protected virtual Simply Do { get { return Simply.Do; } }

        protected string File(string file)
        {
            return Locations.Find(file);
        }

        protected ConfigDef Config(Action<SimplyConfigure> action)
        {
            action(Do.Configure);
            return this;
        }

        protected ConfigDef ConfigFile<T>(Func<SimplyConfigure, IConfiguratorInterface<T, SimplyConfigure>> func, string file)
        {
            func(Do.Configure).FromXmlFile(File(file));
            return this;
        }

        protected ConfigDef ConfigInstance<T>(Func<SimplyConfigure, IConfiguratorInterface<T, SimplyConfigure>> func, T instance)
        {
            func(Do.Configure).FromInstance(instance);
            return this;
        }

        protected abstract void InitLocations(FileLocator paths);
        public abstract ConfigDef ConfigClient();
        public abstract ConfigDef ConfigServer();

        public virtual ConfigDef ConfigAll()
        {
            return ConfigClient().ConfigServer();
        }


        protected string CodeBase(params string[] pathComponents)
        {
            string ret = Path.GetDirectoryName(Uri.UnescapeDataString(
                new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));

            foreach (string component in pathComponents)
                ret = Path.Combine(ret, component);

            return ret;
        }

        public ConfigDef StartClient()
        {
            return StartClient(null);
        }

        public ConfigDef StartClient(Action<Simply> overrides)
        {
            ConfigClient();

            if (overrides != null)
                overrides(Do);

            return this;

        }

        public ConfigDef StartServer(Assembly asm)
        {
            StartServer(asm, null);
            return this;
        }

        public ConfigDef StartServer<T>()
        {
            StartServer<T>(null);
            return this;
        }

        public ConfigDef StartServer<T>(Action<Simply> overrides)
        {
            StartServer(typeof(T).Assembly, overrides);
            return this;
        }

        public ConfigDef StartServer(Assembly asm, Action<Simply> overrides)
        {
            StartClient();
            ConfigServer();

            if (overrides != null)
                overrides(Do);

            Do.InitServer(asm);
            return this;
        }

    }

}
