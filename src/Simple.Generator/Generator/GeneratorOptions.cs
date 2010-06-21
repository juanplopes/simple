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
            return this;
        }


        private void ApplyEnumerable(IGenerator generator, IList<string> values, MemberExpression memberExpression, Type innerType)
        {
            memberExpression.SetValue(generator, values.Select(x => Convert.ChangeType(x, innerType)).ToArray());
        }


        public IGenerator Parse(string parameters)
        {
            var result = _generator();

            foreach (var parser in Parsers)
                parameters = parser.Parse(parameters, result);

            return result;
        }

        public string GeneratorType
        {
            get { return typeof(T).GetRealClassName(); }
        }
    }
}
