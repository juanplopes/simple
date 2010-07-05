using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple
{
    public static class TypesHelper
    {
        public static Type GetValueTypeIfNullable(this Type type)
        {
            return (type.IsGenericType && typeof(Nullable<>) == type.GetGenericTypeDefinition())
               ? type.GetGenericArguments()[0] : type;
        }

        public static bool CanAssign<T, IntoThis>()
        {
            return CanAssign(typeof(T), typeof(IntoThis));
        }

        public static bool CanAssign(this Type type, Type intoThis)
        {
            return intoThis.IsAssignableFrom(type);
        }

        public static object GetBoxedDefaultInstance(this Type type)
        {
            return type.IsValueType && !typeof(void).IsAssignableFrom(type) ?
                Activator.CreateInstance(type) : null;
        }

        public static string GetRealClassName(this Type type)
        {
            return new TypeNameExtractor(type).GetName();
        }

        public static string GetFlatClassName(this Type type)
        {
            string res = GetRealClassName(type);
            foreach (string s in new string[] { "<", ">", ",", "." })
            {
                res = res.Replace(s, "_");
            }
            return res.Replace(" ", "");
        }
    }
}
