using System;
using System.Collections.Generic;

using System.Text;

namespace SimpleLibrary.NUnit
{
    public interface IEntityProvider
    {
        object Populate(int seed);
        bool Compare(object e1, object e2);
    }
}
