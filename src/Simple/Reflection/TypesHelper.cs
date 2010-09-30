using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using System.Reflection;

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

        public static bool CanAssign<IntoThis>(this Type type)
        {
            return typeof(IntoThis).IsAssignableFrom(type);
        }


        public static bool CanAssign(this Type type, Type intoThis)
        {
            return intoThis.IsAssignableFrom(type);
        }

        public static ISettableMemberInfo ToSettable(this IEnumerable<ISettableMemberInfo> members)
        {
            return new CompositeSettableMember(members);
        }

        public static ISettableMemberInfo ToSettable(this MemberInfo member)
        {
            if (member is PropertyInfo)
                return new PropertyInfoWrapper(member as PropertyInfo);

            if (member is FieldInfo)
                return new FieldInfoWrapper(member as FieldInfo);

            return null;
        }

        public static object GetBoxedDefaultInstance(this Type type)
        {
            return type.IsValueType && !typeof(void).IsAssignableFrom(type) ?
                Activator.CreateInstance(type) : null;
        }

        public static Type[] GetTypeArgumentsFor(this Type toTest, Type toImplement)
        {
            foreach (var iface in toTest.GetInterfaces())
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == toImplement)
                    return iface.GetGenericArguments();

            return new Type[0];
        }

        public static string GetRealClassName(this Type type)
        {
            return new TypeNameExtractor().GetName(type);
        }

        public static string GetFlatClassName(this Type type)
        {
            return new TypeNameExtractor().GetFlatName(type, "_");
        }

        internal static IEnumerable<string> SplitProperty(this string propertyPath)
        {
            return propertyPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal static string JoinProperty(this IEnumerable<string> propertyPath)
        {
            return propertyPath.StringJoin(".");
        }
    }
}
