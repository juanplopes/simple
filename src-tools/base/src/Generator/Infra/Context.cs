using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Environment;
using Simple.Generator.Console;
using Simple;

namespace Sample.Project.Generator.Infra
{
    public class Context : ContextBase
    {
        protected override GeneratorResolver Configure(string name, bool defaultContext)
        {
            var resolver = new GeneratorResolver().WithHelp().Define(defaultContext);
            var cfg = new Configurator(name);

            if (!defaultContext)
                cfg.StartServer<ServerStarter>();
            else
                cfg.ConfigClient().ConfigServer();

            return resolver;
        }
    }
}
