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

namespace Sample.Project.Generator.Infra
{
    public static class Program
    {
        public static IContextManager Manager = new ContextManager<Context>();


        static void Main(string[] args)
        {
            if (RootFinder.ChangeToPathOf("generator.findme", "Sample.Project"))
                Console.WriteLine("Found flag file. Changed current directory to:\n'{0}'.", Env.CurrentDirectory);

            for (string command; ReadCommand(out command); )
            {
                Manager.Execute(command);
                Console.WriteLine();
            }
        }

        private static bool ReadCommand(out string command)
        {
            do
            {
                Console.Write(Manager.PromptContext);
                Console.Write(">");
                command = Console.ReadLine();
            } while (command != null && command.Trim() == string.Empty);

            return command != null;
        }
    }
}
