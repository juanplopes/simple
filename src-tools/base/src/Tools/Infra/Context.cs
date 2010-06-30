using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Environment;
using Simple.Generator.Console;
using Simple;

namespace Sample.Project.Tools.Infra
{
    public class Context : ContextBase
    {
        protected override CommandResolver Configure(string name, bool defaultContext)
        {
            var resolver = new CommandResolver().WithHelp().Define(defaultContext);
            var cfg = new Configurator(name);

            cfg.StartServer<ServerStarter>();

            return resolver;
        }
    }
}
