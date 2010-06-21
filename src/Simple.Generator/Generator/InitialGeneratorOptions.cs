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
            Parsers.Add(Tuples.Get(RegexHelper.ListRegex, (MemberExpression)into.Body));
            return this;
        }
    }
}
