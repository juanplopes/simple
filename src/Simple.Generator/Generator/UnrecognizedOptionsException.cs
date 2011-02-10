using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Generator
{
    [Serializable]
    public class UnrecognizedOptionsException : ParserException
    {
        public UnrecognizedOptionsException() { }
        public UnrecognizedOptionsException(string options)
            : base("Unrecognized options: '{0}'.".AsFormatFor(options)) { }


        protected UnrecognizedOptionsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
