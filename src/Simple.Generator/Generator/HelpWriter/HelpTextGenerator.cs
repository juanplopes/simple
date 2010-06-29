using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.HelpWriter
{
    public class HelpTextGenerator : ICommand
    {
        protected CommandResolver Resolver { get; set; }
        protected IList<IHelpWriter> Writers { get; set; }
        public IList<string> OptionNames { get; set; }

        public HelpTextGenerator(CommandResolver resolver, params IHelpWriter[] writers)
        {
            Resolver = resolver;
            Writers = writers;
        }


        public void Execute()
        {
            if (OptionNames.Count == 0)
                foreach (var writer in Writers)
                    writer.Write(Resolver);
            else
                foreach (var writer in Writers)
                    writer.Write(
                        OptionNames.SelectMany(name =>
                            Resolver.GetMeta().Where(
                            x => x.First.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) != -1)));
        }


    }
}
