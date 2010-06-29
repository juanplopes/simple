using System;
using System.Collections.Generic;
using Simple.Patterns;
namespace Simple.Generator.HelpWriter
{
    public interface IHelpWriter
    {
        void Write(IEnumerable<Pair<string, ICommandOptions>> commands);
        void Write(CommandResolver resolver);
    }
}
