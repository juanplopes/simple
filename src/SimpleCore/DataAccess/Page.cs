using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Simple.DataAccess
{
    [Serializable]
    public class Page<T> : ReadOnlyCollection<T>, IPage<T>
    {
        public int TotalCount { get; private set; }

        public int TotalPages(int pageSize)
        {
            if (pageSize > 0)
            {
                return TotalCount / pageSize + ((TotalCount % pageSize == 0) ? 0 : 1);
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
