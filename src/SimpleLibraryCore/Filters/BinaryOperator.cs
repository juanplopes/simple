using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public abstract class BinaryOperator : UnaryOperator
    {
        [DataMember]
        public Filter Filter2 { get; set; }

        protected BinaryOperator(Filter filter1, Filter filter2) : base(filter1)
        {
            Filter2 = filter2;
        }
    }
}
