using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class OrderBy
    {
        [DataMember]
        public bool IsAsc { get; set; }
        [DataMember]
        public string PropertyName { get; set; }

        public OrderBy(string propertyName, bool asc)
        {
            this.PropertyName = propertyName;
            this.IsAsc = asc;
        }

        public static OrderByCollection Asc(string propertyName)
        {
            OrderByCollection col = new OrderByCollection();
            return col.Asc(propertyName);
        }

        public static OrderByCollection Desc(string propertyName)
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
