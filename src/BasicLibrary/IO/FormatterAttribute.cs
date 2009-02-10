using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.IO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatterAttribute : Attribute
    {

        public IFormatter Instance { get; protected set; }

        public FormatterAttribute(Type type, params object[] parameters)
        {
            Instance = (IFormatter)Activator.CreateInstance(type, parameters);
        }

        public FormatterAttribute(string format)
        {
            Instance = new DefaultFormatter(format);
        }
        
    }
}
