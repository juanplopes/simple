using System.Collections.Generic;

namespace Simple.Entities
{
    public interface IGridPage<T> : IPage<T>
    {
        int PageSize { get; }
    }
}
