using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Simple.Configuration.TypeHandlers
{
    public class ByteArrayTypeHandler : AttributeAbleTypeHandler
    {
        public ByteArrayTypeHandler(LoadConfiguration config) : base(config) { }

        protected override bool IsMatch(Type type)
        {
            return typeof(byte[]).IsAssignableFrom(type);
        }
    }
}
