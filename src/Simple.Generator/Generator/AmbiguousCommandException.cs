using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Generator
{
    [Serializable]
    public class AmbiguousCommandException : Exception
    {
        public AmbiguousCommandException() { }
        public AmbiguousCommandException(string command, IEnumerable<IGeneratorOptions> generators)
            : base("Multiple generator found for input '{0}': {1}".AsFormat(
                command, GetParserListString(generators))) { }

        private static string GetParserListString(IEnumerable<IGeneratorOptions> parsers)
        {
            return string.Join(", ", parsers.Select(x => x.GeneratorType).ToArray());
        }

        protected AmbiguousCommandException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
