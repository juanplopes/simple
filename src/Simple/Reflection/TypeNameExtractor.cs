using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Reflection
{
    public class TypeNameExtractor
    {
        public Type ObservedType { get; protected set; }
        public Type[] GenericArguments { get; protected set; }

        protected TypeNameExtractor(Type observedType, Type[] genericArguments)
        {
            if (observedType.IsByRef)
                observedType = observedType.GetElementType();

            ObservedType = observedType;
            GenericArguments = genericArguments;
        }

        public TypeNameExtractor(Type observedType)
            : this(observedType, ExtractGenericArguments(observedType))
        {
        }

        private static Type[] ExtractGenericArguments(Type observedType)
        {
            if (observedType.IsGenericType)
                return observedType.GetGenericArguments();
            else
                return new Type[0];
        }

        protected int WriteInternal(TextWriter writer)
        {

            int skip = WriteParentType(writer);
            int take = ObservedType.GetGenericArguments().Length - skip;

            if (ObservedType == typeof(void))
            {
                writer.Write("void");
                return skip+take;
            }

            var types = GenericArguments.Skip(skip).Take(take).ToArray();

            string args = types.Length > 0
                ? "<" + string.Join(", ", types.Select(x => new TypeNameExtractor(x).GetName()).ToArray()) + ">"
                : "";

            writer.Write(GetReadableTypeName());
            writer.Write(args);
            return skip + take;
        }

        private int WriteParentType(TextWriter writer)
        {

            int skip = 0;
            if (ObservedType.IsNested && !ObservedType.IsGenericParameter)
            {
                skip = new TypeNameExtractor(ObservedType.DeclaringType, GenericArguments).WriteInternal(writer);
                writer.Write(".");
            }
            return skip;
        }

        private string GetReadableTypeName()
        {
            var baseName = ObservedType.Name;
            int index = baseName.IndexOf('`');
            if (index >= 0)
                baseName = baseName.Substring(0, index);
            return baseName;
        }

        public void WriteName(TextWriter writer)
        {
           WriteInternal(writer);
        }

        public string GetName()
        {
            var writer = new StringWriter();
            WriteName(writer);
            return writer.ToString();
        }
    }
}
