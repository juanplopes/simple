using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Generator
{
    [Serializable]
    public class UnrecognizedOptionsException : Exception
    {
        public UnrecognizedOptionsException() { }
        public UnrecognizedOptionsException(string options)
            : base("Unrecognized options: '{0}'.".AsFormat(options)) { }


        protected UnrecognizedOptionsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
