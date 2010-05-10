using System.Collections.Generic;

namespace Simple.Entities
{
    public interface IPage
    {
        int TotalCount { get; }
        int TotalPages(int pageSize);
    }

    public interface IPage<T> : IPage, IList<T>
    {
       
    }
}
