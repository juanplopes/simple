using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLibrary.NUnit
{
    public interface ITypeSeeder
    {
        object GetValue(Type type, int seed);
    }
}
