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
        protected List<Pair<Regex, MemberExpression>> Parsers = 
            new List<Pair<Regex, MemberExpression>>();

        public GeneratorOptions(Func<T> generator)
        {
            this._generator = generator;
        }


        public GeneratorOptions<T> Parameter<P>(string name, Expression<Func<T, P>> into)
        {
            return this;
        }


        public IGenerator Parse(string parameters)
        {
            parameters = parameters.Trim();

            return _generator();
        }

        public string GeneratorType
        {
            get { return typeof(T).GetRealClassName(); }
        }
    }
}
