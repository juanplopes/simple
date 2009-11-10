using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO;
using System.IO;
using System.Reflection;
using Simple.ConfigSource;

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
        public string Environment { protected get; set; }

        public const string Test = "Test";
        public const string Development = "Development";
        public const string Production = "Production";

        public ConfigDef(string env)
        {
            Environment = env;
        }

        protected virtual Simply Do { get { return Simply.Do; } }

        protected string File(string file)
        {
            return Locations.Find(file);
        }

        protected void Config(Action<SimplyConfigure> action)
        {
            action(Do.Configure);
        }

        protected void ConfigFile<T>(Func<SimplyConfigure, IConfiguratorInterface<T, SimplyConfigure>> func, string file)
        {
            func(Do.Configure).FromXmlFile(File(file));
        }

        protected void ConfigInstance<T>(Func<SimplyConfigure, IConfiguratorInterface<T, SimplyConfigure>> func, T instance)
        {
            func(Do.Configure).FromInstance(instance);
        }

        protected abstract void InitLocations(FileLocator paths);
        protected abstract void ConfigClient();
        protected abstract void ConfigServer();

        protected string CodeBase(string path)
        {
            return Path.Combine(
                Path.GetDirectoryName(Uri.UnescapeDataString(
                new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath)), path);
        }

        public void StartClient()
        {
            ConfigClient();
        }

        public void StartServer(Assembly asm)
        {
            StartServer(asm, true, null);
        }


        public void StartServer(Assembly asm, bool wait)
        {
            StartServer(asm, wait, null);
        }


        public void StartServer(Assembly asm, bool wait, Action<Simply> overrides)
        {
            StartClient();
            ConfigServer();

            if (overrides != null)
                overrides(Do);

            Do.InitServer(asm, wait);
        }

    }

}
