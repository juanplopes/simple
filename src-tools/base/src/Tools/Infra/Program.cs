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
            RootFinder.ChangeToPathOf("simple.key", Configurator.DefaultNamespace);
            Console.WriteLine("Dir: '{0}'.", Env.CurrentDirectory);

            var context = new Context();

            for (string command; ReadCommand(out command); )
            {
                context.Execute(command);
            }
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
