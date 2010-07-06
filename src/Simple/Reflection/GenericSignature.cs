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
            var constraints = EnumerateConstraints().ToList();
            if (constraints.Count == 0) return string.Empty;
            else return " where {0} : {1}".AsFormat(TypeNames.GetName(Type), constraints.StringJoin(", "));
        }

        protected IEnumerable<string> EnumerateConstraints()
        {
            if ((Type.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
                yield return "class";

            if ((Type.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
                yield return "struct";

            if (Type.BaseType != typeof(object) && Type.BaseType != typeof(ValueType))
                yield return TypeNames.GetName(Type.BaseType);

            var interfaces = Type.GetInterfaces().Select(x => TypeNames.GetName(x)).ToList();
            interfaces.Sort();
            foreach (var inter in interfaces)
                yield return inter;

            if ((Type.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0 &&
                Type.BaseType != typeof(ValueType))
                yield return "new()";
        }

    }
}
