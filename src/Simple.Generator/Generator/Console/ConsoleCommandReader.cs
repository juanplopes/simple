using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator.Console;

namespace Simple.Generator.Console
{
    public class ConsoleCommandReader
    {
        public ContextBase Context { get; protected set; }
        public bool SafeMode { get; set; }
        public ConsoleCommandReader(ContextBase context, bool safe)
        {
            this.Context = context;
            this.SafeMode = safe;
        }

        public void Run(string[] args)
        {
            if (args.Length == 0)
                for (string command; ReadCommand(out command); )
                    Context.Execute(command, true);

            else
                foreach (var command in args)
                    Context.Execute(command, false);

        }

        private bool ReadCommand(out string command)
        {

            do
            {
                System.Console.WriteLine();
                System.Console.Write(SafeMode ? "safe" : "dev");
                System.Console.Write(">");

                command = System.Console.ReadLine();
            } while (CheckRetry(command));


            return command != null;
        }

        private bool CheckRetry(string command)
        {
            if (command == null) return false;
            if (command.Trim() == string.Empty) return true;

            if (SafeMode)
            {
                System.Console.Write("You are in safe mode. Are you sure (Y/N)? ");
                var answer = System.Console.ReadLine();
                if (answer == null || answer.ToLower().Trim() != "y")
                {
                    System.Console.WriteLine("User aborted.");
                    return true;
                }
            }
            return false;
        }
    }
}
