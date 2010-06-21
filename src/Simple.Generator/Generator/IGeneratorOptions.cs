using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator
{
    public interface IGeneratorOptions
    {
        string GeneratorType { get; }
        IGenerator Parse(string parameters);
    }
}
