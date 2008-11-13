using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class PropertyExpression<T> : Expression
    {
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public T Value { get; set; }
        public PropertyExpression(string propertyName, T value)
        {
            this.PropertyName = propertyName;
            this.Value = value;
        }
    }
}
