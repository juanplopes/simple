using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class InExpression : TypedPropertyExpression<object[]>
    {
        public InExpression(PropertyName propertyName, params object[] values)
            : base(propertyName, values)
        { }
    }
}
