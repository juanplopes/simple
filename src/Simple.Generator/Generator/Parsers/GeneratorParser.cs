using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Simple.Generator.Parsers
{

    public abstract class GeneratorParser<T, P> : IGeneratorParser
    {
        public Regex RegularExpression { get; private set; }
        public string Name { get; private set; }
        public MemberExpression Member { get { return (MemberExpression)Expression.Body; } }

        public Expression<Func<T, P>> Expression { get; private set; }

        public GeneratorParser(bool mustBeExplicit, string name, Expression<Func<T, P>> expression)
        {
            Name = name ?? string.Empty;
            RegularExpression = (!mustBeExplicit) ? Regexes.ListRegex : Regexes.OptionRegex(name);
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
            return match.Groups[Regexes.ValueGroup]
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




}
