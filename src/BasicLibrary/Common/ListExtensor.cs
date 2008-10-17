using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BasicLibrary.Common
{
    public static class ListExtensor
    {
        public static T GetFirst<T>(this IList list)
        {
            if (list.Count > 0) return (T)list[0];
            else return default(T);
        }
    }
}
