using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.Filters
{
    public sealed class PropertyName
    {
        public string Name { get; set; }
        public PropertyName(string name)
        {
            this.Name = name;
        }

        public static implicit operator PropertyName(string name)
        {
            return new PropertyName(name);
        }

        public static implicit operator string(PropertyName prop)
        {
            return prop.Name;
        }

        public PropertyName this[PropertyName property]
        {
            get
            {
                return this.Dot(property);
            }
        }

        public PropertyName Dot(PropertyName property)
        {
            return new PropertyName(this.Name + "." + property.Name);
        }
    }
}
