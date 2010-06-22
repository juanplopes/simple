using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Simple.Generator.Parsers
{
    public class GeneratorListParser<T, P> : GeneratorParser<T, IEnumerable<P>>
    {
        public GeneratorListParser(bool mustBeExplicit, string name, Expression<Func<T, IEnumerable<P>>> expression) : base(mustBeExplicit, name, expression) { }
        protected override void ParseInternal(string match, IList<string> values, IGenerator generator)
        {
            ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator,
                values.Select(x => ConvertValue<P>(x)).ToList());
        }
    }
}
