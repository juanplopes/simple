using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator
{
    [global::System.Serializable]
    public class GeneratorException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public GeneratorException() { }
        public GeneratorException(string message) : base(message) { }
        public GeneratorException(string message, Exception inner) : base(message, inner) { }
        protected GeneratorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
