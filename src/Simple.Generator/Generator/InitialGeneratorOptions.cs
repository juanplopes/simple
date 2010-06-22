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
    public class InitialGeneratorOptions<T> : GeneratorOptions<T>
        where T : IGenerator
    {
        public InitialGeneratorOptions(Func<T> generator) : base(generator) { }

        public GeneratorOptions<T> Argument<P>(string name, Expression<Func<T, P>> into)
        {
            ArgumentParser = new GeneratorValueParser<T, P>(false, name, into);
            return this;
        }

        public GeneratorOptions<T> ArgumentList<P>(string name, Expression<Func<T, IEnumerable<P>>> into)
        {
            ArgumentParser = new GeneratorListParser<T, P>(false, name, into);
            return this;
        }
    }
}
