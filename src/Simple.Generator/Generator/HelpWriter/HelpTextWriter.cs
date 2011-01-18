using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Simple.Patterns;

namespace Simple.Generator.HelpWriter
{
    public class HelpTextWriter : Simple.Generator.HelpWriter.IHelpWriter
    {
        private TextWriter Writer { get; set; }

        public HelpTextWriter(TextWriter writer)
        {
            Writer = writer;
        }

        private void Blank()
        {
            Writer.WriteLine();
        }

        private void WriteCommandName(string name, ICommandOptions command)
        {
            var param = command.Argument;

            if (param != null)
                Writer.WriteLine("{0} <{1}> ({2})", name, param.Name, param.Member.Type.GetRealClassName());
            else
                Writer.WriteLine("{0}", name);
        }

        public void Write(CommandResolver resolver)
        {
            Writer.WriteLine("Available commands: (say 'help <commands>' for more info)");

            foreach (var command in resolver.GetMeta())
            {
                Blank(); 
                WriteCommandName(command.Item1, command.Item2);
                Writer.WriteLine("> Generator: {0}", command.Item2.GeneratorType);
            }
        }

        public void Write(IEnumerable<Tuple<string, ICommandOptions>> commands)
        {
            var list = commands.ToList();

            Writer.WriteLine("Commands found: {0}", list.Count);

            foreach (var cmd in list)
            {
                Blank();

                Writer.Write("Command: ");
                WriteCommandName(cmd.Item1, cmd.Item2);

                var options = cmd.Item2.Options.ToList();

                if (options.Count > 0)
                {
                    Writer.WriteLine("Available options:");
                    foreach (var option in cmd.Item2.Options)
                        Writer.WriteLine("> {0} [{1}]", option.Name, option.Member.Type.GetRealClassName());
                }
                else
                {
                    Writer.WriteLine("No available options.");
                }
            }
        }

    }
}
