using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class PropertyExpression<T> : Expression
    {
        [DataMember]
        public PropertyName PropertyName { get; set; }
        [DataMember]
        public T Value { get; set; }
        public PropertyExpression(PropertyName propertyName, T value)
        {
            propertyName.EnsureNotDotted();

            this.PropertyName = propertyName;
            this.Value = value;
        }
    }
}
