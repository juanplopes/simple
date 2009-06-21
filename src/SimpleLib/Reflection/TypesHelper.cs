using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Reflection
{
    public static class TypesHelper
    {
        public static bool CanAssign<T, IntoThis>()
        {
            return CanAssign(typeof(T), typeof(IntoThis));
        }

        public static bool CanAssign(Type type, Type intoThis)
        {
            return intoThis.IsAssignableFrom(type);
        }
    }
}
