using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public class OrderBy
    {
        [DataMember]
        public bool IsAsc { get; set; }
        [DataMember]
        public PropertyName PropertyName { get; set; }

        public OrderBy(PropertyName propertyName, bool asc)
        {
            propertyName.EnsureNotDotted();
            this.PropertyName = propertyName;
            this.IsAsc = asc;
        }

        public static OrderByCollection Asc(PropertyName propertyName)
        {
            OrderByCollection col = new OrderByCollection();
            return col.Asc(propertyName);
        }

        public static OrderByCollection Desc(PropertyName propertyName)
        {
            OrderByCollection col = new OrderByCollection();
            return col.Desc(propertyName);
        }

        public static OrderByCollection None()
        {
            return new OrderByCollection();
        }
    }
}
