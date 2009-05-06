using System;
using System.Collections.Generic;
using System.Text;
using SimpleGenerator.Definitions;
using Simple.Configuration;
using System.IO;

namespace SimpleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Invalid parameters");
                throw new InvalidConfigurationException();
            }

            if (args[2].Trim().ToLower() == "interface")
            {
                RulesInterfaces.Generator generator = new SimpleGenerator.RulesInterfaces.Generator();
                generator.Generate(FileConfigProvider<GeneratorConfig>.Instance.Get(Path.Combine(args[0], args[1]), null), args[0]);
            }
        }
    }
}
