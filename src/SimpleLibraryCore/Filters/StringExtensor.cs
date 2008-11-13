using System;
using System.Collections.Generic;

using System.Text;

namespace SimpleLibrary.Filters
{
    public static class StringExtensor
    {
        public static string Dot(string property, string prop2)
        {
            return property + "." + prop2;
        }
    }
}
