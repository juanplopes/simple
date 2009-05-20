using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public class IsNullExpression : PropertyExpression
    {
        public IsNullExpression(PropertyName propertyName) : base(propertyName) { }
    }
}
