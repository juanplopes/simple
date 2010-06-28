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

namespace Sample.Project.Generator.Infra
{
    public static class Program
    {
        public static string EnvName = null;
        static ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        static void Main(string[] args)
        {
            PrepareEnvironment();

            var resolver = new GeneratorResolver().WithHelp().Define();

            for (string command; ReadCommand(out command); )
                resolver.Execute(command);
        }

        private static void PrepareEnvironment()
        {
            new Configurator().ConfigClient().ConfigServer();

            if (RootFinder.ChangeToPathOf("generator.findme", "Sample.Project"))
                Console.WriteLine("Found flag file. Changed current directory to:\n'{0}'.", Env.CurrentDirectory);
        }

        private static void Execute(this GeneratorResolver resolver, string command)
        {
            try
            {
                resolver.Resolve(command).Execute();
            }
            catch (GeneratorException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
                Console.WriteLine(e.StackTrace);
            }

            Console.WriteLine();
        }

        private static bool ReadCommand(out string command)
        {
            do
            {
                Console.Write(EnvName ?? "default");
                Console.Write(">");
                command = Console.ReadLine();
            } while (command != null && command.Trim() == string.Empty);

            return command != null;
        }
    }
}
