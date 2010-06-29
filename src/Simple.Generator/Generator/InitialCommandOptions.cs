using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Patterns;
using System.Text.RegularExpressions;
using Simple.Generator.Parsers;

namespace Simple.Generator
{
    public class InitialCommandOptions<T> : CommandOptions<T>
        where T : ICommand
    {
        public InitialCommandOptions(Func<T> generator) : base(generator) { }

        public CommandOptions<T> WithArgument<P>(string name, Expression<Func<T, P>> into)
        {
            ArgumentParser = new ValueParser<T, P>(false, name, into);
            return this;
        }

        public CommandOptions<T> WithArgumentList<P>(string name, Expression<Func<T, IEnumerable<P>>> into)
        {
            ArgumentParser = new ListParser<T, P>(false, name, into);
            return this;
        }
    }
}
