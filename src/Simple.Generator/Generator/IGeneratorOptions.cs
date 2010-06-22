using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Patterns;
using Simple.Generator.Parsers;

namespace Simple.Generator
{
    public interface IGeneratorOptions
    {
        IGeneratorParser Argument { get; }
        IEnumerable<IGeneratorParser> Options { get; }
        string GeneratorType { get; }
        IGenerator Parse(string parameters, bool ignoreExceedingArgs);
    }
}
