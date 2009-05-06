using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [CollectionDataContract]
    public class OrderByCollection : List<OrderBy>
    {
        public OrderByCollection() : base() { }

        public OrderByCollection Asc(PropertyName propertyName)
        {
            this.Add(new OrderBy(propertyName, true));
            return this;
        }

        public OrderByCollection Desc(PropertyName propertyName)
        {
            this.Add(new OrderBy(propertyName, false));
            return this;
        }
    }
}
