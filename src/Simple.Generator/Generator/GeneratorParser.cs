using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Simple.Generator
{
    public interface IGeneratorParser
    {
        string Parse(string args, IGenerator generator);
    }

    public abstract class GeneratorParser<T, P> : IGeneratorParser
    {
        protected Regex RegularExpression { get; private set; }
        protected Expression<Func<T, P>> Expression { get; private set; }

        public GeneratorParser(Regex regex, Expression<Func<T, P>> expression)
        {
            RegularExpression = regex;
            Expression = expression;
        }

        public virtual string Parse(string args, IGenerator generator)
        {
            var match = Match(args);
            var values = ExtractValues(match);
            ParseInternal(match.ToString(), values, generator);
            return RegularExpression.Replace(args, "");
        }
        protected abstract void ParseInternal(string match, IList<string> values, IGenerator generator);

        protected Match Match(string args)
        {
            return RegularExpression.Match(args);
        }

        protected IList<string> ExtractValues(Match match)
        {
            return match.Groups[RegexHelper.ValueGroup]
                .Captures.OfType<Capture>().Select(x => x.Value).ToList();
        }

        protected K ConvertValue<K>(string value)
        {
            if (typeof(K) == typeof(bool))
                value = ReplaceBoolean(value);

            return (K)Convert.ChangeType(value, typeof(K).GetValueTypeIfNullable());
        }

        private string ReplaceBoolean(string value)
        {
            switch (value.ToLower())
            {
                case "+":
                case "1":
                case "t":
                case "y":
                case "s":
                case "yes":
                    return "true";
                case "-":
                case "0":
                case "f":
                case "n":
                case "no":
                    return "false";
                    
                default:
                    return value;
            }
        }
    }

    public class GeneratorValueParser<T, P> : GeneratorParser<T, P>
    {
        public GeneratorValueParser(Regex regex, Expression<Func<T, P>> expression) : base(regex, expression) { }
        protected override void ParseInternal(string match, IList<string> values, IGenerator generator)
        {
            if (values.Count != 1)
                throw new InvalidArgumentCountException(match, 1, values.Count);

            ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator, ConvertValue<P>(values.First()));
        }
    }

    public class GeneratorListParser<T, P> : GeneratorParser<T, IEnumerable<P>>
    {
        public GeneratorListParser(Regex regex, Expression<Func<T, IEnumerable<P>>> expression) : base(regex, expression) { }
        protected override void ParseInternal(string match, IList<string> values, IGenerator generator)
        {
            ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator,
                values.Select(x => ConvertValue<P>(x)).ToList());
        }
    }
}
