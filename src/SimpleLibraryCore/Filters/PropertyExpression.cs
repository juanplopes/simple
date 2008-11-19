using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class PropertyExpression : Expression
    {
        [DataMember]
        public PropertyName PropertyName { get; set; }

        public PropertyExpression(PropertyName propertyName)
        {
            propertyName.EnsureNotDotted();
            this.PropertyName = propertyName;
        }
    }

    [DataContract]
    public class PropertyExpression<T> : PropertyExpression
    {
        [DataMember]
        public T Value { get; set; }

        public PropertyExpression(PropertyName propertyName, T value) : base(propertyName)
        {
            this.Value = value;
        }
    }
}
