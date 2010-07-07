using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.IO;
using System.Reflection;
using Simple.Services;
using System.IO;
using Sample.Project.Config;
using Simple.Generator;
using Simple.Data;

namespace Sample.Project.Environment
{
    public class Configurator : ConfigDef
    {
#if DEBUG
        public static bool IsProduction { get { return false; } }
#else
        public static bool IsProduction { get { return true; } }
#endif


        public const string DefaultNamespace = "Sample.Project";
        public const string SimpleKey = "simple.token";


        protected override void InitLocations(FileLocator paths)
        {
            paths.Add(CodeBase(this.GetType(), "Environment", Environment));
            paths.Add(CodeBase(this.GetType(), "cfg"));
            paths.Add(CodeBase(this.GetType(), "..", "cfg"));
            paths.Add(CodeBase(this.GetType(), "..", "..", "cfg"));
        }

        public override ConfigDef ConfigClient()
        {
            ConfigFile(x => x.Log4net(), "Log4net.config");
            ConfigFile(x => x.The<ApplicationConfig>(), "Application.config");

            Config(x => x.DefaultHost());

            if (!IsTest)
                Do.AddClientHook(x => new HttpIdentityInjector(x));

            return this;
        }

        public override ConfigDef ConfigServer()
        {
            ConfigureNHibernate();
            Config(x => x.Validator(Assembly.GetExecutingAssembly()));
            Do.AddServerHook(x => new TransactionCallHook(x, ConfigKey));
            Do.AddServerHook(x => new DefaultCallHook(x, ConfigKey));
            return this;
        }

        private void ConfigureNHibernate()
        {
            var nhCache = new NHibernateCache(
                GetRootedPath("nhibernate.{0}.cache".AsFormat(Environment)), this.GetType().Assembly);
            var nhConfig = nhCache.Get();

            if (nhConfig != null)
            {
                Do.SetNHibernateConfig(nhConfig);
            }
            else
            {
                ConfigFile(x => x.NHibernate(), "NHibernate.config");
                nhCache.Set(Do.GetNHibernateConfig());
            }
        }

        public Configurator() : this(null) { }
        public Configurator(string env) : this(env, null) { }
        public Configurator(string env, object key)
            : base(env, key)
        {

        }
        public override FileInfo FindKeyFile()
        {
            return DefaultFindKeyFile(SimpleKey, DefaultNamespace);
        }
    }
}
