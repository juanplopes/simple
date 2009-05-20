using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public abstract class Filter
    {
        public AndExpression And(Filter filter)
        {
            return new AndExpression(this, filter);
        }

        public OrExpression Or(Filter filter)
        {
            return new OrExpression(this, filter);
        }

        public NotExpression Negate()
        {
            return new NotExpression(this);
        }
    }
}
