using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.HelpWriter
{
    public class HelpTextGenerator : IGenerator
    {
        protected GeneratorResolver Resolver { get; set; }
        protected IHelpWriter Writer { get; set; }
        public IList<string> OptionNames { get; set; }

        public HelpTextGenerator(GeneratorResolver resolver, IHelpWriter writer)
        {
            Resolver = resolver;
            Writer = writer;
        }

        public void Execute()
        {
            if (OptionNames.Count == 0)
                Writer.Write(Resolver);
            else
                Writer.Write(
                    OptionNames.SelectMany(name =>
                        Resolver.GetMeta().Where(
                        x => x.First.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) != -1)));
        }


    }
}
