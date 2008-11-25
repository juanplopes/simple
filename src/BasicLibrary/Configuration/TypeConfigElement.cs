using System;
using System.Collections.Generic;

using System.Text;

namespace BasicLibrary.Configuration
{
    public class TypeConfigElement : PlainXmlConfigElement, IStringConvertible
    {
        [ConfigElement("name", Default = null)]
        public string Name { get; set; }

        public Type LoadType()
        {
            return Type.GetType(Name);
        }

        void IStringConvertible.LoadFromString(string value)
        {
            Name = value;
            (this as IConfigElement).NotifyLoad("name");
        }

    }
}
