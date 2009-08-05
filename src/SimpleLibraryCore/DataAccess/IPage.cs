using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace SimpleLibrary.DataAccess
{
    public interface IPage : IList
    {
        int TotalCount { get; }
        int TotalPages { get; }
    }

    public interface IPage<T> : IList<T>, IPage
    {
    }
}
