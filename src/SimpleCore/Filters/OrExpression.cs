using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [DataContract]
    public class OrExpression : BinaryOperator
    {
        public OrExpression(Filter filter1, Filter filter2) : base(filter1, filter2) { }
    }
}
