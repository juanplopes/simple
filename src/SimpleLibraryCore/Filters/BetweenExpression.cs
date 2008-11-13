using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class BetweenExpression : Expression
    {
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public object HiValue { get; set; }
        [DataMember]
        public object LoValue { get; set; }

        public BetweenExpression(string propertyName, object lo, object hi)
        {
            this.PropertyName = propertyName;
            this.HiValue = hi;
            this.LoValue = lo;
        }
    }
}
