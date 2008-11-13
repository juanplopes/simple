using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class AndExpression : BinaryOperator
    {
        public AndExpression(Filter filter1, Filter filter2) : base(filter1, filter2) { }
    }
}
