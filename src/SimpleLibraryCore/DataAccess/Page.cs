using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.DataAccess
{
    [DataContract]
    public class Page<T>
    {
        [DataMember]
        public int TotalItems { get; set; }
        [DataMember]
        public IList<T> Items { get; set; }

        public int PageSize { get { return this.Items.Count; } }

        public long TotalPages
        {
            get
            {
                if (PageSize > 0)
                {
                    return TotalItems / PageSize + ((TotalItems % PageSize == 0) ? 0 : 1);
                }
                else
                {
                    return 0;
                }
            }
        }

        public Page(IList<T> items, int totalItems)
        {
            TotalItems = totalItems;
            Items = items;
        }

        public Page() { }
    }
}
