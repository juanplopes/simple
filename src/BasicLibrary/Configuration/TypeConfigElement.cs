using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicLibrary.Configuration
{
    public class TypeConfigElement : PlainXmlConfigElement
    {
        [ConfigElement("name", Default = null)]
        public string Name { get; set; }

        public Type LoadType()
        {
            return Type.GetType(Name);
        }
    }
}
