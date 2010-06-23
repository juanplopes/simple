using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Environment;
using Env = System.Environment;

namespace Sample.Project.Generator.Infra
{
    public static class Program
    {
        static void Main(string[] args)
        {
            new Configurator().ConfigClient().ConfigServer();

            if (RootFinder.ChangeToPathOf("generator.findme", "Sample.Project"))
                Console.WriteLine("Found flag file. Changed current directory to:\n'{0}'.", Env.CurrentDirectory);

            string command;
            var resolver = new GeneratorResolver().WithHelp().Define();

            while (ReadCommand(out command))
                if (!string.IsNullOrEmpty(command.Trim()))
                    resolver.Execute(command);
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
            Console.Write(">");
            command = Console.ReadLine();
            return command.Trim() != "quit";
        }
    }
}
