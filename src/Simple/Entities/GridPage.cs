using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Entities
{
    [Serializable]
    public class GridPage<T> : Page<T>, IGridPage<T>
    {
        public int PageSize { get; protected set; }

        public GridPage(IPage<T> page, int pageSize)
            : base(page, page.TotalCount)
        {
            this.PageSize = pageSize;
        }

        public int TotalPages()
        {
            return base.TotalPages(PageSize);
        }
    }
}
