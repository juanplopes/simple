using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string baseName = type.Name;

            if (type.IsGenericType)
            {
                string args = string.Join(",", new List<string>(
                type.GetGenericArguments().Where(x => x.FullName != null)
                    .Select(x => x.Name)).ToArray());

                int index = baseName.IndexOf('`');
                if (index >= 0)
                    baseName = baseName.Substring(0, index);

                baseName += "<" + args + ">";
            }

            return baseName;
        }

        public static string GetFlatClassName(this Type type)
        {
            string res = GetRealClassName(type);
            foreach (string s in new string[] { "<", ">", "," })
            {
                res = res.Replace(s, "_");
            }
            return res;
        }
    }
}
