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

        private void WriteCommandName(string name, IGeneratorOptions command)
        {
            var param = command.Argument;

            if (param != null)
                Writer.WriteLine("{0} <{1}> ({2})", name, param.Name, param.Member.Type.GetRealClassName());
            else
                Writer.WriteLine("{0}", name);
        }

        public void Write(GeneratorResolver resolver)
        {
            Writer.WriteLine("Available commands: (say 'help <commands>' for more info)");

            foreach (var command in resolver.GetMeta())
            {
                Blank(); 
                WriteCommandName(command.First, command.Second);
                Writer.WriteLine("> Generator: {0}", command.Second.GeneratorType);
            }
        }

        public void Write(IEnumerable<Pair<string, IGeneratorOptions>> commands)
        {
            var list = commands.ToList();

            Writer.WriteLine("Commands found: {0}", list.Count);

            foreach (var cmd in list)
            {
                Blank();

                Writer.Write("Command: ");
                WriteCommandName(cmd.First, cmd.Second);
                Writer.WriteLine("Available options:");

                foreach (var option in cmd.Second.Options)
                    Writer.WriteLine("> {0} [{1}]", option.Name, option.Member.Type.GetRealClassName());
            }
        }

    }
}
