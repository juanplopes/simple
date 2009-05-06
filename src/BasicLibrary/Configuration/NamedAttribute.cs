using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Simple.Configuration
{
    public class NamedAttribute : Attribute
    {
        public string Name { get; set; }
        protected NamedAttribute(string name)
        {
            this.Name = name;
        }
    }
}
