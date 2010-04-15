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
        public const string MigrationProvider = "System.Data.SqlClient";

        public Configurator() : this(null) { }
        public Configurator(string env) : base(env ?? Development) { }

        protected override void InitLocations(FileLocator paths)
        {
            paths.Add(CodeBase("Environment", Environment));
            paths.Add(CodeBase("cfg"));
            paths.Add(CodeBase("..", "cfg"));
            paths.Add(CodeBase("..", "..", "cfg"));
        }

        public override void ConfigClient()
        {
            ConfigFile(x => x.Log4net(), "Log4net.config");
            ConfigFile(x => x.The<ApplicationConfig>(), "Application.config");

            if (Environment == Test)
            {
                Config(x => x.DefaultHost());
            }
            else
            {
                ConfigFile(x => x.Remoting(), "Remoting.config");
                Do.AddClientHook(x => new HttpIdentityInjector(x));
            }
        }

        public override void ConfigServer()
        {
            ConfigFile(x => x.NHibernate(), "NHibernate.config");
            Config(x => x.Validator(Assembly.GetExecutingAssembly()));
            Do.AddServerHook(x => new TransactionCallHook(x));
            Do.AddServerHook(x => new DefaultCallHook(x, null));
        }
    }
}
