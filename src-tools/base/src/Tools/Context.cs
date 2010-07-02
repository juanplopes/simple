using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Environment;
using Simple.Generator.Console;
using Simple;
using Simple.Patterns;
using Sample.Project.Tools.Infra;

namespace Sample.Project.Tools
{
    public class Context : ContextBase
    {
        public static IDisposable Development { get { return Get(Configurator.Development); } }
        public static IDisposable Test { get { return Get(Configurator.Test); } }

        public static IDisposable Get(string name)
        {
            var logger = Simply.Do.Log<Context>();
            logger.InfoFormat("Entering: '{0}'...", name);
            var ctx = Simply.KeyContext(name);
            return new DisposableAction(() =>
            {
                logger.InfoFormat("Exiting: '{0}'...", name);
                ctx.Dispose();
            });
        }

        public Context() : base(Default.Namespace) { }

        protected override CommandResolver Configure()
        {
            var resolver = new CommandResolver().WithHelp().Define(Configurator.IsProduction);

            if (Configurator.IsProduction)
                InternalConfigure(null);
            else
                InternalConfigure(Configurator.Development, Configurator.Test);

            return resolver;
        }

        protected override void OnBeforeExecute(ICommand commandObject, string command, bool interactive)
        {
            if (interactive && Configurator.IsProduction)
            {
                Console.Write("You are in production environment. Are you sure (Y/N)? ");
                var answer = Console.ReadLine();
                if (answer == null || answer.ToLower().Trim() != "y")
                    throw new ParserException("User aborted.");
            }
            base.OnBeforeExecute(commandObject, command, interactive);
        }

        protected void InternalConfigure(params string[] names)
        {
            names = names ?? new string[] { null };
            foreach (var name in names)
                new Configurator(name, name).StartServer<ServerStarter>();
        }
    }
}
