using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.TestBase
{
    public interface ITypeSeeder
    {
        object GetValue(Type type, int seed);
    }
}
