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
        public Configurator() : this(null) { }
        public Configurator(string env) : base(env ?? Development) { }

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

            //if (Environment != Test)
            //    Do.AddClientHook(x => new HttpIdentityInjector(x));

            return this;
        }

        public override ConfigDef ConfigServer()
        {
            ConfigFile(x => x.NHibernate(), "NHibernate.config");
            Config(x => x.Validator(Assembly.GetExecutingAssembly()));
            Do.AddServerHook(x => new TransactionCallHook(x));
            Do.AddServerHook(x => new DefaultCallHook(x, null));
            return this;
        }
    }
}
