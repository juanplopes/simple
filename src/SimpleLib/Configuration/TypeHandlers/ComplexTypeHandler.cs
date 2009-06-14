using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Simple.Configuration2.TypeHandlers
{
    public class ComplexTypeHandler : AttributeAbleTypeHandler
    {
        public ComplexTypeHandler(LoadConfiguration config) : base(config) { }
        
        protected override bool IsMatch(Type type)
        {
            return typeof(IConfigElement).IsAssignableFrom(type);
        }
    }
}
