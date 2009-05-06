using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [DataContract]
    public abstract class UnaryOperator : Operator
    {
        [DataMember]
        public Filter Filter1 { 
            get { return base._filters[0]; }
            set { base._filters[0] = value; }
        }
        protected UnaryOperator(Filter filter1) : base(filter1) { }
    }
}
