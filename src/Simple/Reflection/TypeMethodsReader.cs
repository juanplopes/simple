using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class TypeMethodsReader
    {
        public Type Type { get; protected set; }
        public IList<MethodSignature> Methods { get; protected set; }
        public HashSet<string> Namespaces { get; protected set; }

        public TypeMethodsReader(Type type)
        {
            Type = type;
            Methods = EnumerateMethods(type).ToList();
            Namespaces = new HashSet<string>();
        }

        public TypeMethodsReader InitializeNamespaces()
        {
            foreach (var method in Methods)
            {
                method.MakeSignature();
                foreach (var ns in method.Namespaces)
                    Namespaces.Add(ns);
            }
            return this;
        }

        private static IEnumerable<MethodSignature> EnumerateMethods(Type type)
        {

            foreach (var method in type.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                yield return new MethodSignature(method);

        }



    }
}
