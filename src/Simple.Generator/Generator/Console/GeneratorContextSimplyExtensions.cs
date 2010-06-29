using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator.Console;

namespace Simple
{
    public static class GeneratorContextSimplyExtensions
    {
        internal static IContext _context = null;

        public static IContext GetGeneratorContext(this Simply simply)
        {
            return _context;
        }
    }
}
