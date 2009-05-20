using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public abstract class BinaryOperator : Operator
    {
        [DataMember]
        public Filter Filter1
        {
            get { return base._filters[0]; }
            set { base._filters[0] = value; }
        }

        [DataMember]
        public Filter Filter2
        {
            get { return base._filters[1]; }
            set { base._filters[1] = value; }
        }



        protected BinaryOperator(Filter filter1, Filter filter2)
            : base(filter1, filter2)
        {
        }
    }
}
