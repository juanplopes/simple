using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Simple.Entities
{
    [Serializable]
    public class Page<T> : ReadOnlyCollection<T>, IPage<T>
    {
        public virtual int TotalCount { get; private set; }

        public virtual int TotalPages(int pageSize)
        {
            if (pageSize > 0)
            {
                return (int)Math.Ceiling((decimal)TotalCount / pageSize);
            }
            else
            {
                return 0;
            }

        }

        public Page(IList<T> items, int totalCount)
            : base(items)
        {
            TotalCount = totalCount;
        }
    }
}
