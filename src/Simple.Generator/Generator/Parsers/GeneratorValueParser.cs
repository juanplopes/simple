using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Simple.Generator.Parsers
{
    public class GeneratorValueParser<T, P> : GeneratorParser<T, P>
    {
        public GeneratorValueParser(bool mustBeExplicit, string name, Expression<Func<T, P>> expression) : base(mustBeExplicit, name, expression) { }
        protected override void ParseInternal(string match, IList<string> values, IGenerator generator)
        {
            if (values.Count != 1)
                throw new InvalidArgumentCountException(match, 1, values.Count);

            ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator, ConvertValue<P>(values.First()));
        }
    }
}
