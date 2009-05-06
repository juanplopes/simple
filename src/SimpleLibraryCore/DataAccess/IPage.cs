using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace Simple.DataAccess
{
    public interface IPage<T> : IList<T>
    {
        int TotalCount { get; }
        int TotalPages { get; }
    }
}
