using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Simple.Reflection
{
    public class TypeNameExtractor
    {
        public HashSet<string> Namespaces { get; set; }

        public TypeNameExtractor()
        {
            Namespaces = new HashSet<string>();
        }

        protected int AppendTypeName(StringBuilder builder, Type type)
        {
            var generic = type.IsGenericType ? type.GetGenericArguments() : new Type[0];
            return AppendTypeName(builder, type, generic);
        }

        protected int AppendTypeName(StringBuilder builder, Type type, Type[] genericArguments)
        {
            type = VerifyRefType(type);

            int skip = AppendParentTypes(builder, type, genericArguments);
            int take = type.GetGenericArguments().Length - skip;
            var generic = genericArguments.Skip(skip).Take(take);

            if (type == typeof(void))
            {
                builder.Append("void");
                return skip + take;
            }

            AppendReadableTypeName(builder, type);
            AppendTypeArguments(builder, generic);

            return skip + take;
        }

        private void AppendTypeArguments(StringBuilder builder, IEnumerable<Type> generic)
        {

            if (generic.Any())
            {
                builder.Append("<");
                generic.EagerForeach(x => AppendTypeName(builder, x), x => builder.Append(", "));
                builder.Append(">");
            }
        }

        private static Type VerifyRefType(Type type)
        {
            if (type.IsByRef)
                type = type.GetElementType();
            return type;
        }

        private int AppendParentTypes(StringBuilder builder, Type type, Type[] genericArguments)
        {

            int skip = 0;
            if (type.IsNested && !type.IsGenericParameter)
            {
                skip = AppendTypeName(builder, type.DeclaringType, genericArguments);
                builder.Append(".");
            }
            return skip;
        }

        private void AppendReadableTypeName(StringBuilder builder, Type type)
        {
            var baseName = type.Name;
            int index = baseName.IndexOf('`');
            if (index >= 0)
                baseName = baseName.Substring(0, index);
            builder.Append(baseName);
        }


        public string GetName(Type type)
        {
            var builder = new StringBuilder();
            AppendTypeName(builder, type);
            return builder.ToString();
        }

        public string GetFlatName(Type type, string replacement)
        {
            return Regex.Replace(GetName(type), "[<>,.]", replacement).Replace(" ", "");
        }
    }
}
