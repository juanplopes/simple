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
        public object ConfigKey { get; protected set; }

        public const string Test = "Test";
        public bool IsTest { get { return Is(Test); } }

        public const string Development = "Development";
        public bool IsDevelopment { get { return Is(Development); } }

        public virtual string DefaultEnvironment { get { return Development; } }
        public bool IsDefault { get { return Is(DefaultEnvironment); } }

        public static bool EnvEquals(string value1, string value2)
        {
            return string.Compare(value1, value2, true) == 0;
        }

        public bool Is(string env)
        {
            return EnvEquals(this.Environment, env ?? DefaultEnvironment);
        }


        public ConfigDef(string env, object key)
        {
            Environment = env ?? DefaultEnvironment;
            ConfigKey = key;
        }

        protected virtual Simply Do { get { return Simply.Do[ConfigKey]; } }

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
        public abstract FileInfo FindKeyFile();

        public string GetRootedPath(string relativePath)
        {
            var key = FindKeyFile();
            return Path.Combine((key != null && key.Exists) ? key.Directory.FullName :
                System.Environment.CurrentDirectory, relativePath);
        }

        public bool ChangeToRoot()
        {
            var file = FindKeyFile();
            if (file != null)
            {
                Simply.Do.Log(this).DebugFormat("Found key file, changing to {0}..", file.Directory.FullName);
                System.Environment.CurrentDirectory = file.Directory.FullName;
                return true;
            }
            else
            {
                Simply.Do.Log(this).Warn("Key file not found.");
                return false;
            }
        }

        protected FileInfo DefaultFindKeyFile(string file, string content)
        {
            var fileInfo = RootFinder.Find(file, content);
            if (!fileInfo.Exists)
                fileInfo = RootFinder.Find(
                    new DirectoryInfo(CodeBase(this.GetType())), file, content);
            if (fileInfo.Exists) return fileInfo;
            else return null;
        }

        public virtual ConfigDef ConfigAll()
        {
            return ConfigClient().ConfigServer();
        }


        protected static string CodeBase(Type type, params string[] pathComponents)
        {
            string ret = Path.GetDirectoryName(Uri.UnescapeDataString(
                new Uri(type.Assembly.CodeBase).AbsolutePath));

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
