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

        public static object GetBoxedDefaultInstance(Type type)
        {
            return type.IsValueType && !typeof(void).IsAssignableFrom(type) ?
                Activator.CreateInstance(type) : null;
        }

        public static string GetRealClassName(Type type)
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

        public static string GetFlatClassName(Type type)
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
