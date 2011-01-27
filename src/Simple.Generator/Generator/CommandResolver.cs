using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simple.Patterns;
using Simple.Generator.HelpWriter;
using System.IO;

namespace Simple.Generator
{
    public class CommandResolver
    {
        protected List<CommandRegistry> Parsers =
            new List<CommandRegistry>();

        public IEnumerable<CommandRegistry> GetMeta()
        {
            return Parsers;
        }


        public InitialCommandOptions<T> Register<T>(params string[] cmds)
            where T : ICommand, new()
        {
            return Register(() => new T(), cmds);
        }


        public InitialCommandOptions<T> Register<T>(Func<T> generator, params string[] cmds)
            where T : ICommand
        {
            var opts = new InitialCommandOptions<T>(generator);
            cmds = cmds.Select(x => x.CorrectInput()).ToArray();

            foreach (var cmd in cmds)
                Parsers.Add(new CommandRegistry(cmd, opts));

            return opts;
        }

        public CommandResolver WithHelp(params IHelpWriter[] writers)
        {
            this.Register(() => new HelpTextGenerator(this, writers), "help")
                .WithArgumentList("commands", x => x.OptionNames);
            return this;
        }
        public CommandResolver WithHelp(params TextWriter[] writers)
        {
            return WithHelp(writers.Select(x => new HelpTextWriter(x)).ToArray());
        }

        public CommandResolver WithHelp()
        {
            return WithHelp(System.Console.Out);
        }


        public ICommand Resolve(string cmdLine)
        {
            return Resolve(cmdLine, false);
        }

        public ICommand Resolve(string cmdLine, bool ignoreExceedingArgs)
        {
            cmdLine = cmdLine.CorrectInput();

            var parser = FindParser(cmdLine);
            cmdLine = cmdLine.Remove(cmdLine.IndexOf(parser.Command), parser.Command.Length);
            var generator = parser.Parser.Parse(cmdLine, ignoreExceedingArgs);

            return generator;
        }

        private CommandRegistry FindParser(string cmdLine)
        {
            var parsers = Parsers.Where(x => Regex.IsMatch(cmdLine, x.Command.ToRegexFormat(true))).ToList();

            if (parsers.Count > 1)
                throw new AmbiguousCommandException(cmdLine, parsers.Select(x => x.Parser));

            if (parsers.Count == 0)
                throw new InvalidCommandException(cmdLine);

            var parser = parsers.First();
            return parser;
        }

    }
}
