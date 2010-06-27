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
    public class GeneratorResolver
    {
        protected List<Pair<string, IGeneratorOptions>> Parsers =
            new List<Pair<string, IGeneratorOptions>>();

        public IEnumerable<Pair<string, IGeneratorOptions>> GetMeta()
        {
            return Parsers;
        }


        public InitialGeneratorOptions<T> Register<T>(params string[] cmds)
            where T : IGenerator, new()
        {
            return Register(() => new T(), cmds);
        }


        public InitialGeneratorOptions<T> Register<T>(Func<T> generator, params string[] cmds)
            where T : IGenerator
        {
            var opts = new InitialGeneratorOptions<T>(generator);
            cmds = cmds.Select(x => x.CorrectInput()).ToArray();

            foreach (var cmd in cmds)
                Parsers.Add(new Pair<string, IGeneratorOptions>(cmd, opts));

            return opts;
        }

        public GeneratorResolver WithHelp(params IHelpWriter[] writers)
        {
            this.Register(() => new HelpTextGenerator(this, writers), "help")
                .WithArgumentList("commands", x => x.OptionNames);
            return this;
        }
        public GeneratorResolver WithHelp(params TextWriter[] writers)
        {
            return WithHelp(writers.Select(x => new HelpTextWriter(x)).ToArray());
        }

        public GeneratorResolver WithHelp()
        {
            return WithHelp(Console.Out);
        }


        public IGenerator Resolve(string cmdLine)
        {
            return Resolve(cmdLine, false);
        }

        public IGenerator Resolve(string cmdLine, bool ignoreExceedingArgs)
        {
            cmdLine = cmdLine.CorrectInput();

            var parser = FindParser(cmdLine);
            cmdLine = cmdLine.Remove(cmdLine.IndexOf(parser.First), parser.First.Length);
            var generator = parser.Second.Parse(cmdLine, ignoreExceedingArgs);

            if (generator != null)
                Simply.Do.Log(this).InfoFormat("Found generator: {0}...", generator.GetType().Name);
            return generator;
        }

        private Pair<string, IGeneratorOptions> FindParser(string cmdLine)
        {
            var parsers = Parsers.Where(x => Regex.IsMatch(cmdLine, x.First.ToRegexFormat(true))).ToList();

            if (parsers.Count > 1)
                throw new AmbiguousCommandException(cmdLine, parsers.Select(x => x.Second));

            if (parsers.Count == 0)
                throw new InvalidCommandException(cmdLine);

            var parser = parsers.First();
            return parser;
        }

    }
}
