using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator
{
    [Serializable]
    public class InvalidArgumentCountException : ParserException
    {
        public InvalidArgumentCountException() { }
        public InvalidArgumentCountException(string command, int expected, int actual)
            : base("Invalid argument count in '{0}'. Expected: {1}. Found: {2}".AsFormat(
                command, expected, actual)) { }


        protected InvalidArgumentCountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
