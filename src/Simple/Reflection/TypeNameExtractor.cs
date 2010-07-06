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

        public TypeNameExtractor() : this(new HashSet<string>()) { }

        public TypeNameExtractor(HashSet<string> visitedNamespaces)
        {
            Namespaces = visitedNamespaces;
        }


        protected int AppendTypeName(StringBuilder builder, Type type, bool fullyQualified)
        {
            var generic = type.IsGenericType ? type.GetGenericArguments() : new Type[0];
            return AppendTypeName(builder, type, generic, fullyQualified);
        }

        protected int AppendTypeName(StringBuilder builder, Type type, Type[] genericArguments, bool fullyQualified)
        {
            type = VerifyRefType(type);

            int skip = AppendParentTypes(builder, type, genericArguments, fullyQualified);
            int take = type.GetGenericArguments().Length - skip;
            var generic = genericArguments.Skip(skip).Take(take);

            AppendReadableTypeName(builder, type, fullyQualified && !type.IsNested);
            AppendTypeArguments(builder, generic, fullyQualified);

            return skip + take;
        }

        private void AppendTypeArguments(StringBuilder builder, IEnumerable<Type> generic, bool fullyQualified)
        {
            if (generic.Any())
            {
                builder.Append("<");
                generic.EagerForeach(x => AppendTypeName(builder, x, fullyQualified), x => builder.Append(", "));
                builder.Append(">");
            }
        }

        private static Type VerifyRefType(Type type)
        {
            if (type.IsByRef)
                type = type.GetElementType();
            return type;
        }

        private int AppendParentTypes(StringBuilder builder, Type type, Type[] genericArguments, bool fullyQualified)
        {

            int skip = 0;
            if (type.IsNested && !type.IsGenericParameter)
            {
                skip = AppendTypeName(builder, type.DeclaringType, genericArguments, fullyQualified);
                builder.Append(".");
            }
            return skip;
        }

        private void AppendReadableTypeName(StringBuilder builder, Type type, bool fullyQualified)
        {
            var baseName = type.Name;
            int index = baseName.IndexOf('`');
            if (index >= 0)
                baseName = baseName.Substring(0, index);


            if (type == typeof(void))
            {
                builder.Append("void");
            }
            else
            {
                if (fullyQualified)
                {
                    builder.Append(type.Namespace);
                    builder.Append(".");
                }
                builder.Append(baseName);
            }

            Namespaces.Add(type.Namespace);
        }


        public string GetName(Type type)
        {
            return GetName(type, false);
        }


        public string GetName(Type type, bool fullyQualified)
        {
            var builder = new StringBuilder();
            AppendTypeName(builder, type, fullyQualified);
            return builder.ToString();
        }

        public string GetFlatName(Type type, string replacement)
        {
            return Regex.Replace(GetName(type), "[<>,.]", replacement).Replace(" ", "");
        }
    }
}
