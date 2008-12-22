using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLibrary.Filters
{
    public class InExpression : TypedPropertyExpression<object[]>
    {
        public InExpression(PropertyName propertyName, params object[] values)
            : base(propertyName, values)
        { }
    }
}
