using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;

namespace Sample.Project.Generator
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string command;
            var resolver = new GeneratorResolver().WithHelp().Define();

            while (ReadCommand(out command))
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
