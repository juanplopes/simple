using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class ClassSignature
    {
        public Type Type { get; protected set; }
        public IList<MethodSignature> Methods { get; protected set; }
        public HashSet<string> Namespaces { get { return TypeNames.Namespaces; } }
        public TypeNameExtractor TypeNames { get; protected set; }

        public ClassSignature(Type type, HashSet<string> namespaces)
        {
            this.TypeNames = new TypeNameExtractor(namespaces);
            this.Type = type;
            this.Methods = EnumerateMethods().ToList();
        }
        public ClassSignature(Type type) : this(type, new HashSet<string>()) { }

        public ClassSignature InitializeNamespaces()
        {
            MakeImplementingSignature();
            foreach (var method in Methods)
                method.MakeSignature();
            return this;
        }
        public string MakeImplementingSignature()
        {
            return MakeImplementingSignature(null);
        }

        public string MakeImplementingSignature(params string[] except)
        {
            var builder = new StringBuilder();
            var interfaces = Type.GetInterfaces();
            interfaces.Select(x => TypeNames.GetName(x)).Except(except ?? new string[0]).OrderBy(x => x).EagerForeach(
                x => builder.Append(x),
                x => builder.Append(", "));

            var types = interfaces.SelectMany(x => x.GetGenericArguments().Where(y => y.IsGenericParameter)).Distinct();

            types.EagerForeach(x =>
                builder.Append(new GenericSignature(x, Namespaces).MakeConstraints()));

            return builder.ToString();
        }


        protected IEnumerable<MethodSignature> EnumerateMethods()
        {

            foreach (var method in Type.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsDefined(typeof(LeaveMeAloneAttribute), false)).Where(x =>
                {
                    var b = x.GetBaseDefinition();
                    return b == x || b.DeclaringType.IsInterface;
                }))

                yield return new MethodSignature(method, Namespaces);

        }



    }
}
