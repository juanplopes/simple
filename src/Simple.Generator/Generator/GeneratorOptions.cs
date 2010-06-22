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
    public class GeneratorOptions<T> : IGeneratorOptions
        where T : IGenerator
    {
        Func<T> _generator;
        protected IGeneratorParser ArgumentParser = null;
        protected List<IGeneratorParser> OptionParsers = new List<IGeneratorParser>();
        public GeneratorOptions(Func<T> generator)
        {
            this._generator = generator;
        }


        public GeneratorOptions<T> Option<P>(string name, Expression<Func<T, P>> into)
        {
            OptionParsers.Add(new GeneratorValueParser<T, P>(true, name, into));
            return this;
        }

        public GeneratorOptions<T> OptionList<P>(string name, Expression<Func<T, IEnumerable<P>>> into)
        {
            OptionParsers.Add(new GeneratorListParser<T, P>(true, name, into));
            return this;
        }



        private void ApplyEnumerable(IGenerator generator, IList<string> values, MemberExpression memberExpression, Type innerType)
        {
            memberExpression.SetValue(generator, values.Select(x => Convert.ChangeType(x, innerType)).ToArray());
        }


        public IGenerator Parse(string parameters, bool ignoreExceedingArgs)
        {
            var result = _generator();
            
            if (ArgumentParser != null)
                parameters = Argument.Parse(parameters, result);
            
            foreach (var parser in OptionParsers)
                parameters = parser.Parse(parameters, result);

            parameters = parameters.Trim();
            if (parameters.Length > 0)
            {
                if (!ignoreExceedingArgs)
                    throw new UnrecognizedOptionsException(parameters);
                else
                    Simply.Do.Log(this).Warn(string.Format("Unrecognized options: {0}", parameters));
            }

            return result;
        }

        public string GeneratorType
        {
            get { return typeof(T).GetRealClassName(); }
        }

        #region IGeneratorOptions Members

        public IEnumerable<IGeneratorParser> Options
        {
            get { return OptionParsers; }
        }

        public IGeneratorParser Argument
        {
            get { return ArgumentParser; }
        }

        #endregion
    }
}
