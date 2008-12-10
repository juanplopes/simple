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

        public object CreateInstance(params object[] objs)
        {
            if (Name == null) return null;

            return Activator.CreateInstance(LoadType(), objs);
        }

        void IStringConvertible.LoadFromString(string value)
        {
            Name = value;
            (this as IConfigElement).NotifyLoad("name");
        }


        public static object CreateInstance(TypeConfigElement element, params object[] objs)
        {
            if (element == null) return null;

            return element.CreateInstance(objs);
        }
    }
}
