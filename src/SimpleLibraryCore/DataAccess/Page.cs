using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace SimpleLibrary.DataAccess
{
    [DataContract]
    public class Page<T> : ReadOnlyCollection<T>, IPage<T>
    {
        [DataMember]
        public int TotalCount { get; private set; }

        public int TotalPages
        {
            get
            {
                if (Count > 0)
                {
                    return TotalCount / Count + ((TotalCount % Count == 0) ? 0 : 1);
                }
                else
                {
                    return 0;
                }
            }
        }

        public Page(IEnumerable<T> items, int totalCount) : base(new List<T>(items))
        {
            TotalCount = totalCount;
        }
    }
}
