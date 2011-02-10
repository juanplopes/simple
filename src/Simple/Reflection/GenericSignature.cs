using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class GenericSignature
    {
        public Type Type { get; protected set; }
        public TypeNameExtractor TypeNames { get; protected set; }

        public GenericSignature(Type type, HashSet<string> namespaces)
        {
            this.Type = type;
            this.TypeNames = new TypeNameExtractor(namespaces);
        }
        public GenericSignature(Type type) : this(type, new HashSet<string>()) { }

        public string MakeConstraints()
        {
            return CreateConstraintString();
        }

        protected string CreateConstraintString()
        {
            var constraints = EnumerateConstraints(Type).ToList();
            if (constraints.Count == 0) return string.Empty;
            else return " where {0} : {1}".AsFormatFor(TypeNames.GetName(Type), constraints.StringJoin(", "));
        }

        protected IEnumerable<string> EnumerateConstraints(Type type)
        {
            if ((type.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
                yield return "class";

            if ((type.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
                yield return "struct";

            if (type.BaseType != typeof(object) && type.BaseType != typeof(ValueType))
                yield return TypeNames.GetName(type.BaseType);

            var interfaces = type.GetInterfaces().Select(x => TypeNames.GetName(x)).ToList();
            interfaces.Sort();
            foreach (var inter in interfaces)
                yield return inter;

            if ((type.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0 &&
                type.BaseType != typeof(ValueType))
                yield return "new()";
        }

    }
}
