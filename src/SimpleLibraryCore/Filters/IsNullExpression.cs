using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class IsNullExpression : PropertyExpression
    {
        public IsNullExpression(PropertyName propertyName) : base(propertyName) { }
    }
}
