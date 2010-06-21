using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Patterns;
using System.Text.RegularExpressions;

namespace Simple.Generator
{
    public class InitialGeneratorOptions<T> : GeneratorOptions<T>
        where T : IGenerator
    {
        public InitialGeneratorOptions(Func<T> generator) : base(generator) { }

        public GeneratorOptions<T> Argument<P>(Expression<Func<T, P>> into)
        {
            Parsers.Add(new GeneratorValueParser<T, P>(RegexHelper.ListRegex, into));
            return this;
        }

        public GeneratorOptions<T> ArgumentList<P>(Expression<Func<T, IEnumerable<P>>> into)
        {
            Parsers.Add(new GeneratorListParser<T, P>(RegexHelper.ListRegex, into));
            return this;
        }
    }
}
