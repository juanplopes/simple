using System;
using System.Collections.Generic;

using System.Text;

namespace Simple.TestBase
{
    public interface IEntityProvider
    {
        object Populate(int seed);
        bool Compare(object e1, object e2);
    }
}
