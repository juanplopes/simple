using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Patterns;
using System.Text.RegularExpressions;

namespace Simple.Generator
{
    public class GeneratorOptions<T> : IGeneratorOptions
        where T : IGenerator
    {
        Func<T> _generator;
        protected List<IGeneratorParser> Parsers = new List<IGeneratorParser>();
        public GeneratorOptions(Func<T> generator)
        {
            this._generator = generator;
        }


        public GeneratorOptions<T> Option<P>(string name, Expression<Func<T, P>> into)
        {
            Parsers.Add(new GeneratorValueParser<T, P>(RegexHelper.OptionRegex(name), into));
            return this;
        }

        public GeneratorOptions<T> OptionList<P>(string name, Expression<Func<T, IEnumerable<P>>> into)
        {
            Parsers.Add(new GeneratorListParser<T, P>(RegexHelper.OptionRegex(name), into));
            return this;
        }



        private void ApplyEnumerable(IGenerator generator, IList<string> values, MemberExpression memberExpression, Type innerType)
        {
            memberExpression.SetValue(generator, values.Select(x => Convert.ChangeType(x, innerType)).ToArray());
        }


        public IGenerator Parse(string parameters, bool ignoreExceedingArgs)
        {
            var result = _generator();

            foreach (var parser in Parsers)
                parameters = parser.Parse(parameters, result);

            parameters = parameters.Trim();
            if (parameters.Length > 0)
            {
                string message = string.Format("unrecognized options: {0}", parameters);
                if (!ignoreExceedingArgs)
                    throw new ArgumentException(message);
                else
                    Simply.Do.Log(this).Warn(message);
            }

            return result;
        }

        public string GeneratorType
        {
            get { return typeof(T).GetRealClassName(); }
        }
    }
}
