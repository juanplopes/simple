using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [CollectionDataContract]
    public class OrderByCollection : List<OrderBy>
    {
        public OrderByCollection() : base() { }

        public OrderByCollection Asc(string propertyName)
        {
            this.Add(new OrderBy(propertyName, true));
            return this;
        }

        public OrderByCollection Desc(string propertyName)
        {
            this.Add(new OrderBy(propertyName, false));
            return this;
        }
    }
}
