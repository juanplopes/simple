using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace Simple.Filters
{
    [Serializable]
    public class InExpression : TypedPropertyExpression<IList>
    {
        public InExpression(PropertyName propertyName, params object[] values)
            : base(propertyName, new ArrayList(values))
        { }

        public InExpression(PropertyName propertyName, ICollection collection)
            : base(propertyName, new ArrayList(collection))
        { }
    }
}
