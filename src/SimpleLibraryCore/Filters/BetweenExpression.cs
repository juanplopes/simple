using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class BetweenExpression : Expression
    {
        [DataMember]
        public PropertyName PropertyName { get; set; }
        [DataMember]
        public object HiValue { get; set; }
        [DataMember]
        public object LoValue { get; set; }

        public BetweenExpression(PropertyName propertyName, object lo, object hi)
        {
            propertyName.EnsureNotDotted();

            this.PropertyName = propertyName;
            this.HiValue = hi;
            this.LoValue = lo;
        }
    }
}
