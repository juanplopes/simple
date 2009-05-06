using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration.TypeHandlers
{
    public class NullableTypeHandler : BasicTypeHandler
    {
        public NullableTypeHandler(LoadConfiguration config) : base(config) { }

        protected override bool IsMatch(Type type)
        {
            return type.IsGenericType && typeof(Nullable<>).IsAssignableFrom(type.GetGenericTypeDefinition()) && typeof(IConvertible).IsAssignableFrom(type.GetGenericArguments()[0]);
        }
    }
}
