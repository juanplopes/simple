using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.NUnit
{
    public interface IEntityProvider<T>
    {
        T Populate(int seed);
        bool Compare(T e1, T e2);
    }
}
