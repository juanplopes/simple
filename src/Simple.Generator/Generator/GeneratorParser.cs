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
            var values = ExtractValues(args);
            ParseInternal(values, generator);
            return RegularExpression.Replace(args, "");
        }
        protected abstract void ParseInternal(IList<string> values, IGenerator generator);

        protected IList<string> ExtractValues(string args)
        {
            return RegularExpression.Match(args).Groups[RegexHelper.ValueGroup]
                .Captures.OfType<Capture>().Select(x => x.Value).ToList();
        }
    }

    public class GeneratorValueParser<T, P> : GeneratorParser<T, P>
    {
        public GeneratorValueParser(Regex regex, Expression<Func<T, P>> expression) : base(regex, expression) { }
        protected override void ParseInternal(IList<string> values, IGenerator generator)
        {
            if (values.Count != 1)
                throw new ArgumentException(string.Format("invalid number of arguments: {0}", values.Count));

            ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator, values.First());
        }
    }

    public class GeneratorListParser<T, P> : GeneratorParser<T, IEnumerable<P>>
    {
        public GeneratorListParser(Regex regex, Expression<Func<T, IEnumerable<P>>> expression) : base(regex, expression) { }
        protected override void ParseInternal(IList<string> values, IGenerator generator)
        {
            ExpressionHelper.SetValue((MemberExpression)Expression.Body, generator,
                values.Select(x => (P)Convert.ChangeType(x, typeof(P).GetValueTypeIfNullable())).ToList());
        }
    }
}
