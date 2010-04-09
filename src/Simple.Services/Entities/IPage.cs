using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

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
