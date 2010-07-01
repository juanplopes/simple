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

namespace Sample.Project.Environment
{
    public class Configurator : ConfigDef
    {
        public const string DefaultNamespace = "Sample.Project";

#if DEBUG
        public static bool IsProduction { get { return false; } }
#else
        public static bool IsProduction { get { return true; } }
#endif

        public Configurator() : this(null) { }
        public Configurator(string env) : this(env, null) { }
        public Configurator(string env, object key) : base(env, key) { }

        protected override void InitLocations(FileLocator paths)
        {

            paths.Add(CodeBase("Environment", Environment));
            paths.Add(CodeBase("cfg"));
            paths.Add(CodeBase("..", "cfg"));
            paths.Add(CodeBase("..", "..", "cfg"));
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
            ConfigFile(x => x.NHibernate(), "NHibernate.config");
            Config(x => x.Validator(Assembly.GetExecutingAssembly()));
            Do.AddServerHook(x => new TransactionCallHook(x, ConfigKey));
            Do.AddServerHook(x => new DefaultCallHook(x, ConfigKey));
            return this;
        }
    }
}
