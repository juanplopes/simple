using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public class IsNotNullExpression : PropertyExpression
    {
        public IsNotNullExpression(PropertyName propertyName) : base(propertyName) { }
    }
}
