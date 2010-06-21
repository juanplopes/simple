using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simple.Patterns;

namespace Simple.Generator
{
    public class GeneratorResolver
    {
        protected List<Pair<string, IGeneratorOptions>> Parsers = 
            new List<Pair<string, IGeneratorOptions>>();

        //new migration
        //scaffold t_clients_%
        //scaffold * +replace
        //scaffold(table1, table2, -whatever) +noreplace
        //gsvc(table1, table2) 

        public InitialGeneratorOptions<T> Register<T>(Func<T> generator, params string[] cmds)
            where T : IGenerator
        {
            var opts = new InitialGeneratorOptions<T>(generator);
            cmds = cmds.Select(x => x.CorrectInput()).ToArray();

            foreach (var cmd in cmds)
                Parsers.Add(new Pair<string, IGeneratorOptions>(cmd, opts));

            return opts;
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
            return parser.Second.Parse(cmdLine, ignoreExceedingArgs);
        }

        private Pair<string, IGeneratorOptions> FindParser(string cmdLine)
        {
            var parsers = Parsers.Where(x => Regex.IsMatch(cmdLine, x.First.ToRegexFormat(true))).ToList();

            if (parsers.Count > 1)
                throw new ArgumentException("Multiple generator found for input '{0}': {1}".AsFormat(
                    cmdLine, GetParserListString(parsers)));

            if (parsers.Count == 0)
                throw new ArgumentException("No generator found for input '{0}'".AsFormat(cmdLine));

            var parser = parsers.First();
            return parser;
        }

        private static string GetParserListString(List<Pair<string, IGeneratorOptions>> parsers)
        {
            return string.Join(", ", parsers.Select(x => x.Second.GeneratorType).ToArray());
        }
    }
}
