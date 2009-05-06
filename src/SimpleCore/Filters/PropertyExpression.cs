using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Simple.Filters
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
}
