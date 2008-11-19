using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class BetweenExpression : PropertyExpression
    {
        [DataMember]
        public object HiValue { get; set; }
        [DataMember]
        public object LoValue { get; set; }
         
        public BetweenExpression(PropertyName propertyName, object lo, object hi) : base(propertyName)
        {
            this.HiValue = hi;
            this.LoValue = lo;
        }
    }
}
