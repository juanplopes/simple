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
        public static IDisposable Development { get { return Simply.KeyContext(Configurator.Development); } }
        public static IDisposable Test { get { return Simply.KeyContext(Configurator.Test); } }

        public Context() : base(Default.DefaultNamespace) { }

        protected override CommandResolver Configure()
        {
            var resolver = new CommandResolver().WithHelp().Define(Configurator.IsProduction);

            if (Configurator.IsProduction)
            {
                InternalConfigure(null);
            }
            else
            {
                InternalConfigure(Configurator.Development);
                InternalConfigure(Configurator.Test);
            }

            return resolver;
        }

        protected void InternalConfigure(string name)
        {
            new Configurator(name, name).StartServer<ServerStarter>();
        }
    }
}
