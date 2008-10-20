using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public abstract class UnaryOperator : Expression
    {
        [DataMember]
        public Filter Filter1 { get; set; }

        protected UnaryOperator(Filter filter1)
        {
            Filter1 = filter1;
        }
    }
}
