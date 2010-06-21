using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Generator
{
    public class InitialGeneratorOptions<T> : GeneratorOptions<T>
        where T : IGenerator
    {
        public InitialGeneratorOptions(Func<T> generator) : base(generator) { }

        public GeneratorOptions<T> List<P>(Expression<Func<T, IEnumerable<P>>> into)
        {
            return this;
        }
    }
}
