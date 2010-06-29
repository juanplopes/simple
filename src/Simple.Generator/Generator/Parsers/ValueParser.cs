using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Simple.Generator.Parsers
{
    public class ValueParser<T, P> : CommandParser<T, P>
    {
        public ValueParser(bool mustBeExplicit, string name, Expression<Func<T, P>> expression) : base(mustBeExplicit, name, expression) { }
        protected override void ParseInternal(string match, IList<string> values, ICommand generator)
        {
            if (values.Count > 1)
                throw new InvalidArgumentCountException(match, 1, values.Count);

            if (values.Count == 1)
                ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator, ConvertValue<P>(values.First()));
        }
    }
}
