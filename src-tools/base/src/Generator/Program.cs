using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;

namespace Sample.Project.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            var resolver = new GeneratorResolver().WithHelp();

            while (ReadCommand(out command))
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
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                Console.WriteLine();
            }


        }

        static bool ReadCommand(out string command)
        {
            Console.Write(">");
            command = Console.ReadLine();
            return command.Trim() != "quit";
        }
    }
}
