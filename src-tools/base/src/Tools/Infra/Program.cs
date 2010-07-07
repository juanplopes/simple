using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Environment;
using Env = System.Environment;
using Simple;
using log4net;
using System.Reflection;
using Simple.Generator.Console;

namespace Sample.Project.Tools.Infra
{
    public static class Program
    {

        static void Main(string[] args)
        {
            RootFinder.ChangeToPathOf("simple.token", Configurator.DefaultNamespace);
            Console.WriteLine("Dir: '{0}'.", Env.CurrentDirectory);

            var context = new Context();

            if (args.Length == 0)
                for (string command; ReadCommand(out command); )
                    context.Execute(command, true);
            else
                foreach (var command in args)
                    context.Execute(command, false);
        }

        private static bool ReadCommand(out string command)
        {
            do
            {
                Console.WriteLine();
                Console.Write(Configurator.IsProduction ? "production" : "development");
                Console.Write(">");
                command = Console.ReadLine();
            } while (command != null && command.Trim() == string.Empty);

            return command != null;
        }
    }
}
