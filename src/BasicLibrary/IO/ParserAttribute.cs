using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParserAttribute : Attribute
    {
        public Type ParserType { get; protected set; }
        public object[] Params { get; protected set; }

        public ParserAttribute(Type type, params object[] parameters)
        {
            ParserType = type;
            Params = parameters;
        }

        public IParser CreateInstance()
        {
            return (IParser)Activator.CreateInstance(ParserType, Params);
        }
    }
}
