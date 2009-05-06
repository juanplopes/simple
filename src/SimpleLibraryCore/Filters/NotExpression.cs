using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [DataContract]
    public class NotExpression : UnaryOperator
    {
        public NotExpression(Filter filter) : base(filter) { }
    }
}
